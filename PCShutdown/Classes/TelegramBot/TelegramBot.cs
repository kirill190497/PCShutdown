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
using System.Collections;
using Telegram.Bots.Http;
using System.Linq;
using System.Windows.Forms;
using Telegram.Bot.Types.Enums;
using Newtonsoft.Json;

namespace PCShutdown.Classes.TelegramBot
{
    internal class ShutdownTelegramBot
    {
        public static List<string> SupportedCommands = new() 
        {
            ShutdownTask.TaskType.RebootPC.ToString(),
            ShutdownTask.TaskType.ShutdownPC.ToString(),
            ShutdownTask.TaskType.Sleep.ToString(),
            ShutdownTask.TaskType.Lock.ToString(),
            ShutdownTask.TaskType.Unlock.ToString(),
            ShutdownTask.TaskType.VolumeMute.ToString(),
            ShutdownTask.TaskType.MediaPause.ToString(),
            ShutdownTask.TaskType.ScreenOff.ToString(),
            ShutdownTask.TaskType.ScreenOn.ToString(),
            ShutdownTask.TaskType.Cancel.ToString(),
            ShutdownTask.TaskType.Screenshot.ToString(),
            //ShutdownTask.GetTranslatedTypeName(ShutdownTask.TaskType.RebootPC),
            //ShutdownTask.GetTranslatedTypeName(ShutdownTask.TaskType.RebootPC),
            //ShutdownTask.GetTranslatedTypeName(ShutdownTask.TaskType.RebootPC),

        };


        static ITelegramBotClient bot = new TelegramBotClient(ShutdownApp.Cfg.Telegram.BotToken);

        static Strings S = ShutdownApp.Translation.Lang.Strings;

        private static InlineKeyboardMarkup YesNoAnswer(string action)
        {
            List<List<InlineKeyboardButton>> rows = new();
            InlineKeyboardButton btn = new(S.Yes);
            btn.CallbackData = action;
            InlineKeyboardButton no = new(S.No);
            no.CallbackData = S.No;
            InlineKeyboardButton cancel = new(S.CancelTasks);
            cancel.CallbackData = ShutdownTask.TaskType.Cancel.ToString();

            rows.Add(new() { btn, no });
            rows.Add(new() { cancel });

            var keyboard = new InlineKeyboardMarkup(rows);
            return keyboard;
        }
        private static IReplyMarkup MainKeyboard()
        {
            var rows = new List<List<KeyboardButton>>();

            var menu = ShutdownApp.Cfg.TelegramMenu;

            foreach (var row in menu) 
            {
                var k_row = new List<KeyboardButton>();
                foreach (var item in row)
                {
                    k_row.Add(new KeyboardButton(ShutdownTask.GetTranslatedTypeName(item)));
                }
                rows.Add(k_row);
            }
            
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

        public static async Task OnCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            await bot.AnswerCallbackQueryAsync(callbackQuery.Id, $"{S.YouSelected} {callbackQuery.Data}");
            
            if (callbackQuery.Data.StartsWith("screenshot"))
            {
                await botClient.DeleteMessageAsync(callbackQuery.Message!.Chat, callbackQuery.Message!.MessageId);
                var path = Path.Combine(Screenshot.DirPath, callbackQuery.Data);
                var file = System.IO.File.Open(path, FileMode.Open);
                _ = await botClient.SendDocumentAsync(callbackQuery.Message!.Chat, new InputFileStream(file, callbackQuery.Data), caption: callbackQuery.Data);
                file.Close();
                
            }
            if (callbackQuery.Data == "clear_screenshots")
            {
                Screenshot.DeleteFiles();
                await botClient.DeleteMessageAsync(callbackQuery.Message!.Chat, callbackQuery.Message!.MessageId);
                _ = await botClient.SendTextMessageAsync(callbackQuery.Message!.Chat, S.CommandDone);
            }
            if (callbackQuery.Data == ShutdownTask.TaskType.Cancel.ToString())
            {
                Server.AddTask(ShutdownTask.TaskType.Cancel, DateTime.Now, S.TasksCanceled);
                await botClient.DeleteMessageAsync(callbackQuery.Message!.Chat, callbackQuery.Message!.MessageId);
            }
            if (callbackQuery.Data == $"{ShutdownTask.TaskType.RebootPC}!")
            {
                Server.AddTask(ShutdownTask.TaskType.RebootPC, DateTime.Now, S.CommandDone);
            }
            if (callbackQuery.Data == $"{ShutdownTask.TaskType.ShutdownPC}!")
            {
                Server.AddTask(ShutdownTask.TaskType.ShutdownPC, DateTime.Now, S.CommandDone);
            }
            if (callbackQuery.Data == $"{ShutdownTask.TaskType.Sleep}!")
            {
                Server.AddTask(ShutdownTask.TaskType.Sleep, DateTime.Now, S.CommandDone);
            }
            if (callbackQuery.Data == S.No)
            {
                await botClient.DeleteMessageAsync(callbackQuery.Message!.Chat, callbackQuery.Message!.MessageId);
            }
        }

        public static async Task OnUpdate(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            
            await (update switch
            {
                { CallbackQuery: { } callbackQuery } => OnCallbackQuery(botClient, callbackQuery),
                
                _ => HandleUpdateAsync(botClient, update, cancellationToken: cancellationToken)
            });
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {

                var message = update.Message;
                if (message.Chat.Id == ShutdownApp.Cfg.Telegram.Admin)
                {

                    if (message.Text.ToLower() == "/start")
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, "Start", replyMarkup: MainKeyboard());
                        return;
                    }
                    else if (message.Text.ToLower() == "/menu")
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, "Menu", replyMarkup: MainKeyboard());
                        return;
                    }
                    else if (message.Text.ToLower() == "/screenshot" || message.Text == ShutdownTask.GetTranslatedTypeName(ShutdownTask.TaskType.Screenshot))
                    {
                        //_ = await botClient.SendTextMessageAsync(message.Chat, Screenshot.Save(), cancellationToken: cancellationToken);
                        var path = Screenshot.Save();
                        var file = System.IO.File.Open(path, FileMode.Open);
                        var filename = path.Split("\\")[^1];

                        _ = await botClient.SendDocumentAsync(message.Chat, new InputFileStream(file, filename), caption: filename);
                        file.Close();
                        //Screenshot.DeleteFile();
                        return;
                    }
                    else if (message.Text.ToLower() == "/list_screenshots")
                    {
                        var kb = new List<List<InlineKeyboardButton>>();
                        string txt = "";
                        var files = Screenshot.FilesList();
                        if (files.Count() != 0)
                        {
                            foreach (var file in files)
                            {
                                InlineKeyboardButton btn = new(file);
                                btn.CallbackData = file;
                                var lst = new List<InlineKeyboardButton>
                            {
                                btn
                            };
                                kb.Add(lst);

                            }
                            InlineKeyboardButton clear = new("Clear list");
                            clear.CallbackData = "clear_screenshots";
                            kb.Add(new List<InlineKeyboardButton> { clear });
                            _ = await botClient.SendTextMessageAsync(message.Chat, "Screenshot list", replyMarkup: new InlineKeyboardMarkup(kb));
                        }
                        else
                        {
                            _ = await botClient.SendTextMessageAsync(message.Chat, "Screenshot list empty");
                        }

                        return;
                    }
                    else if (message.Text == ShutdownTask.GetTranslatedTypeName(ShutdownTask.TaskType.Lock))
                    {
                        //var msg = await botClient.SendTextMessageAsync(message.Chat, "Команда отправлена");
                        _ = await botClient.SendTextMessageAsync(message.Chat, S.EnterLockPin, replyMarkup: new ForceReplyMarkup());
                        return;
                    }
                    else if (message.Text == ShutdownTask.GetTranslatedTypeName(ShutdownTask.TaskType.Unlock))
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, S.EnterUnlockPin, replyMarkup: new ForceReplyMarkup());

                        return;
                    }
                    else if (message.Text == ShutdownTask.GetTranslatedTypeName(ShutdownTask.TaskType.MediaPause))
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, S.CommandDone);
                        Server.AddTask(ShutdownTask.TaskType.MediaPause, DateTime.Now);
                        return;
                    }
                    else if (message.Text == ShutdownTask.GetTranslatedTypeName(ShutdownTask.TaskType.VolumeMute))
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, S.CommandDone);
                        Server.AddTask(ShutdownTask.TaskType.VolumeMute, DateTime.Now);
                        return;
                    }
                    else if (message.Text == ShutdownTask.GetTranslatedTypeName(ShutdownTask.TaskType.Sleep))
                    {
                        string q = ShutdownTask.GetTranslatedTypeName(ShutdownTask.TaskType.Sleep);
                        _ = await botClient.SendTextMessageAsync(message.Chat, $"{q}?", replyMarkup: YesNoAnswer($"{ShutdownTask.TaskType.Sleep}!"));
                        return;
                    }
                    else if (message.Text == ShutdownTask.GetTranslatedTypeName(ShutdownTask.TaskType.ShutdownPC))
                    {
                        string q = ShutdownTask.GetTranslatedTypeName(ShutdownTask.TaskType.ShutdownPC);
                        _ = await botClient.SendTextMessageAsync(message.Chat, $"{q}?", replyMarkup: YesNoAnswer($"{ShutdownTask.TaskType.ShutdownPC}!"));

                        return;
                    }
                    else if (message.Text == ShutdownTask.GetTranslatedTypeName(ShutdownTask.TaskType.RebootPC))
                    {
                        string q = ShutdownTask.GetTranslatedTypeName(ShutdownTask.TaskType.RebootPC);
                        _ = await botClient.SendTextMessageAsync(message.Chat, $"{q}?", replyMarkup: YesNoAnswer($"{ShutdownTask.TaskType.RebootPC}!"));

                        return;
                    }
                    else if (message.Text == $"{ShutdownTask.GetTranslatedTypeName(ShutdownTask.TaskType.Sleep)}!")
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, message.Text, replyMarkup: MainKeyboard());
                        Server.AddTask(ShutdownTask.TaskType.Sleep, DateTime.Now, "Sleep via Telegram");
                        return;
                    }
                    else if (message.Text == $"{ShutdownTask.GetTranslatedTypeName(ShutdownTask.TaskType.ShutdownPC)}!")
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, message.Text, replyMarkup: MainKeyboard());
                        Server.AddTask(ShutdownTask.TaskType.ShutdownPC, DateTime.Now, "Shutdown PC via Telegram");
                        return;
                    }
                    else if (message.Text == $"{ShutdownTask.GetTranslatedTypeName(ShutdownTask.TaskType.RebootPC)}!")
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, message.Text, replyMarkup: MainKeyboard());
                        Server.AddTask(ShutdownTask.TaskType.RebootPC, DateTime.Now, "Reboot PC via Telegram");
                        return;
                    }
                    else if (message.Text == $"{ShutdownTask.GetTranslatedTypeName(ShutdownTask.TaskType.Cancel)}")
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, message.Text, replyMarkup: MainKeyboard());
                        Server.AddTask(ShutdownTask.TaskType.Cancel, DateTime.Now, S.TasksCanceled);
                        return;
                    }
                    else if (message.Text == $"{ShutdownTask.GetTranslatedTypeName(ShutdownTask.TaskType.ScreenOff)}")
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, S.CommandDone, replyMarkup: MainKeyboard());
                        Server.AddTask(ShutdownTask.TaskType.ScreenOff, DateTime.Now, S.CommandDone);
                    }
                    else if (message.Text == $"{ShutdownTask.GetTranslatedTypeName(ShutdownTask.TaskType.ScreenOn)}")
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, S.CommandDone, replyMarkup: MainKeyboard());
                        Server.AddTask(ShutdownTask.TaskType.ScreenOn, DateTime.Now, S.TasksCanceled);
                    }
                    else if (message.ReplyToMessage != null)
                    {
                        if (message.ReplyToMessage.Text == S.EnterLockPin)
                        {
                            _ = await botClient.SendTextMessageAsync(message.Chat, "Pin: " + message.Text, replyMarkup: MainKeyboard());
                            Server.AddTask(ShutdownTask.TaskType.Lock, DateTime.Now, message.Text);
                            return;
                        }
                        else if (message.ReplyToMessage.Text == S.EnterUnlockPin)
                        {
                            _ = await botClient.SendTextMessageAsync(message.Chat, "Pin: " + message.Text, replyMarkup: MainKeyboard());
                            Server.AddTask(ShutdownTask.TaskType.Unlock, DateTime.Now, message.Text);
                            return;
                        }
                        else 
                        {
                            _ = await botClient.SendTextMessageAsync(message.Chat, S.NotImplemented, replyMarkup: MainKeyboard());
                            return; 
                        }
                    }
                    else
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, S.NotImplemented, replyMarkup: MainKeyboard());
                        return;
                    }
                    

                }
                else
                {

                    _ = await botClient.ForwardMessageAsync(ShutdownApp.Cfg.Telegram.Admin, message.Chat, message.MessageId);
                    // _ = await botClient.SendTextMessageAsync(ShutdownApp.Cfg.TelegramAdmin, $"От: {message.Chat.FirstName} {message.Chat.LastName} ({message.Chat.Username}) \n{message.Text}", replyMarkup: MainKeyboard());
                    try
                    {
                        _ = await botClient.SendTextMessageAsync(message.Chat, "You have no access! ");
                        _ = await botClient.SendTextMessageAsync(message.Chat, "https://github.com/kirill190497/PCShutdown");
                    }
                    catch (Exception)
                    {

                    }
                    return;
                }


                

            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var json = JsonConvert.SerializeObject(exception, Formatting.Indented);
            // await Task.Run(() => MessageBox.Show(json));

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
                OnUpdate,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
        }

    }
}
