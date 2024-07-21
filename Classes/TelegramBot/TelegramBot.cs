using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bots.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;

using System.IO;
using Windows.AI.MachineLearning;

namespace PCShutdown.Classes.TelegramBot
{
    internal class ShutdownTelegramBot
    {
        static ITelegramBotClient bot = new TelegramBotClient(Properties.Settings.Default.TelegramBotToken);

        private static ReplyKeyboardMarkup YesNoAnswer(string action)
        {
            
            List<List<KeyboardButton>> rows = new()
            {
                new() { new(action), new("Cancel")}
            };

            var keyboard = new ReplyKeyboardMarkup(rows);
            return keyboard;
        }
        private static IReplyMarkup MainKeyboard()
        {
            var rows = new List<List<KeyboardButton>>();

            var menu = Properties.Settings.Default.TelegramMenu;

            foreach (var row in menu) 
            {
                var k_row = new List<KeyboardButton>();
                foreach (var item in row)
                {
                    k_row.Add(new KeyboardButton(item));
                }
                rows.Add(k_row);
            }
            /* {
                new() { new("Lock"), new("Unlock") },
                new() { new("Screenshot") },
                new() { new("Sleep"), new("Shutdown") },
                new() { new("Cancel") }
            };*/
            IReplyMarkup keyboard;
            if (rows.Count != 0)
            {
                keyboard = new ReplyKeyboardMarkup(rows);
            }
            else
            {
                keyboard = new ReplyKeyboardRemove();
            }

            return keyboard;
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                
                var message = update.Message;
                if (message.Chat.Id == Properties.Settings.Default.TelegramAdmin)
                {
                    if (message.Text.ToLower() == "/start")
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, "Start", replyMarkup: MainKeyboard());
                        return;
                    }
                    if (message.Text.ToLower() == "/menu")
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, "Menu", replyMarkup: MainKeyboard());
                        return;
                    }
                    if (message.Text.ToLower() == "/screenshot" || message.Text == "Screenshot")
                    {
                        //_ = await botClient.SendTextMessageAsync(message.Chat, Screenshot.Save(), cancellationToken: cancellationToken);
                        var path = Screenshot.Save();
                        var file = System.IO.File.Open(path, FileMode.Open);
                        var filename = path.Split("\\")[^1];
                        
                        _ = await botClient.SendDocumentAsync(message.Chat, new InputFileStream(file, filename), caption: filename);
                        file.Close();
                        Screenshot.DeleteFile();
                        return;
                    }
                    if (message.Text == "Lock")
                    {
                        //var msg = await botClient.SendTextMessageAsync(message.Chat, "Команда отправлена");
                        _ = await botClient.SendTextMessageAsync(message.Chat, "Lock pin (numbers)", replyMarkup: new ForceReplyMarkup());
                        return;
                    }
                    if (message.Text == "Unlock")
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, "Unlock pin (numbers)", replyMarkup: new ForceReplyMarkup());

                        return;
                    }
                    if (message.Text == "Pause")
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, "Команда отправлена");
                        Server.AddTask(ShutdownTask.TaskType.MediaPause, DateTime.Now);
                        return;
                    }
                    if (message.Text == "Mute")
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, "Команда отправлена");
                        Server.AddTask(ShutdownTask.TaskType.VolumeMute, DateTime.Now);
                        return;
                    }
                    if (message.Text == "Sleep")
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, "Sleep mode?", replyMarkup: YesNoAnswer("Sleep mode!"));

                        return;
                    }
                    if (message.Text == "Shutdown")
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, "Shutdown PC?", replyMarkup: YesNoAnswer("Shutdown PC!"));

                        return;
                    }
                    if (message.Text == "Sleep mode!")
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, "Sleep mode!", replyMarkup: MainKeyboard());
                        Server.AddTask(ShutdownTask.TaskType.Sleep, DateTime.Now, "Sleep via Telegram");
                        return;
                    }
                    if (message.Text == "Shutdown PC!")
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, "Shutdown PC!", replyMarkup: MainKeyboard());
                        Server.AddTask(ShutdownTask.TaskType.ShutdownPC, DateTime.Now, "Shutdown PC via Telegram");
                        return;
                    }
                    if (message.Text == "Cancel")
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, "Cancel", replyMarkup: MainKeyboard());
                        Server.AddTask(ShutdownTask.TaskType.Cancel, DateTime.Now, "Cancel tasks");
                        return;
                    }
                    if (message.ReplyToMessage != null)
                    {
                        if (message.ReplyToMessage.Text == "Lock pin (numbers)")
                        {
                            _ = await botClient.SendTextMessageAsync(message.Chat, "Pin: " + message.Text, replyMarkup: MainKeyboard());
                            Server.AddTask(ShutdownTask.TaskType.Lock, DateTime.Now, message.Text);
                            return;
                        }
                        if (message.ReplyToMessage.Text == "Unlock pin (numbers)")
                        {
                            _ = await botClient.SendTextMessageAsync(message.Chat, "Pin: " + message.Text, replyMarkup: MainKeyboard());
                            Server.AddTask(ShutdownTask.TaskType.Unlock, DateTime.Now, message.Text);
                            return;
                        }
                    }
                    

                }
                else
                {

                    _ = await botClient.ForwardMessageAsync(Properties.Settings.Default.TelegramAdmin, message.Chat, message.MessageId);
                    // _ = await botClient.SendTextMessageAsync(Properties.Settings.Default.TelegramAdmin, $"От: {message.Chat.FirstName} {message.Chat.LastName} ({message.Chat.Username}) \n{message.Text}", replyMarkup: MainKeyboard());
                    //_ = await botClient.SendTextMessageAsync(message.Chat, "You have no access!");
                    return;
                }


                

            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            await Task.Run(() => { }, cancellationToken);
            
        }

        public static void Run()
        {
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
        }

    }
}
