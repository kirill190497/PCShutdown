using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using AudioSwitcher.AudioApi.CoreAudio;
using Windows.Networking.BackgroundTransfer;
using AudioSwitcher.AudioApi;
using System.Security.AccessControl;

namespace PCShutdown.Classes
{
    partial class Server
    {
        private class Volume
        {
            public int Level { get; set; }
            public bool Mute { get; set; }
        }

        private class Args
        {
            public string Text { get; set; }
            public bool Success { get; set; }
            public string Password { get; set; }
            //public string Mode { get; set; }
            public string Alert_Type { get; set; }
            public string Button { get; set; }
            public string Script { get; set; }
            public string TemplateName { get; set; }
            public string Volume_Level {  get; set; }
            public object Value { get; set; }
            public int ErrorCode { get; set; } = 0;

        }

        public static int localPort = ShutdownApp.Cfg.ServerPort;
        private static bool Running = false;

        public static List<ShutdownTask> TaskList = new();
        private static Thread WatchdogThread;
        private static Thread WebThread;

        public static void Start()
        {
            try
            {
                Running = true;
                WebThread = new(new ThreadStart(Listen));
                WebThread.Start();
                WatchdogThread = new(new ThreadStart(TaskWatchdog));
                //WatchdogThread.Start();
                
            }
            catch (Exception ex)
            {
                ShutdownApp.ShowToast(ShutdownApp.Translation.Lang.Strings.ExceptionOccurred + ": " + ex.ToString() + "\n  " + ex.Message);
            }
        }

        public static void Stop()
        {
            Running = false;

        }


        public static void AddTask(ShutdownTask.TaskType type, DateTime date, string comment = "")
        {
            if (TaskList.Count == 0)
            {
                if (WatchdogThread.ThreadState == System.Threading.ThreadState.Stopped) 
                {
                    WatchdogThread = new Thread(new ThreadStart(TaskWatchdog));
                }
                WatchdogThread.Start();
            }
            int id = 0;
            if (TaskList.Count > 0)
            {
                id = TaskList[^1].ID + 1;
            }
            TaskList.Add(new ShutdownTask(id, type, date, comment));

        }

        public static void RemoveTaskByID(int id)
        {
            TaskList.Remove(TaskList.Find(x => x.ID == id));
        }

        public static void RemoveAllTasks()
        {
            TaskList.Clear();

        }

        public static List<ShutdownTask> GetTasksList()
        {
            return TaskList;
        }

        private static async void TaskWatchdog()
        {
            while (Running && TaskList.Count > 0)
            {
                if (TaskList.Count > 0)
                {
                    await Task.Run(() =>
                    {
                        var list = TaskList.ToArray();
                        foreach (ShutdownTask task in list)
                        {
                            var span = task.Date - DateTime.Now;
                            if (span.TotalSeconds < 10)
                            {
                                ExecCommand(task.Type, 30, task.Comment);
                                RemoveTaskByID(task.ID);
                            }
                        }
                        Thread.Sleep(1000);
                    });
                }



            }
        }

        private static async void Listen()
        {
            HttpListener listener = new();
            listener.Prefixes.Add("http://+:" + localPort + "/");


            try
            {
                listener.Start();
            }
            catch (HttpListenerException e)
            {
                ShutdownApp.ShowToast(ShutdownApp.Translation.Lang.Strings.LaunchError +" \n" + e.Message, ShutdownApp.Translation.Lang.Strings.ErrorOccurred);
                Running = false;
                Application.Exit();
            }



            while (Running)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                string password;
                string action;
                string answer;
                string message;
                long time;
                string responseMode;
                string hardwareAddress;
                int delay = ShutdownApp.Cfg.Delay;



                if (request.HttpMethod == "GET")
                {
                    password = request.QueryString.Get("password");
                    action = request.QueryString.Get("action");
                    message = request.QueryString.Get("message");
                    responseMode = request.QueryString.Get("response");
                    hardwareAddress = request.QueryString.Get("hwid");
                    time = request.QueryString.Get("time") == null ? 0 : long.Parse(request.QueryString.Get("time"));
                    string password_arg = password != null ? "?password=" + password : "";
                    ShutdownTask.TaskType actionType;
                    StringBuilder ss = new(256);
                    //List<Tuple<string, object>> args = new();
                    Args args;

                    if (action == null && (password == ShutdownApp.Cfg.Password || !ShutdownApp.Cfg.PasswordCheck))
                    {
                        Volume volume = new()
                        {
                            Level = Convert.ToInt32(AudioManager.GetMasterVolume()),
                            Mute = AudioManager.GetMasterVolumeMute()
                        };
                        args = new()
                        {
                            Text = ShutdownApp.Translation.Lang.Strings.ActionNotSet,
                            Password = password,
                            TemplateName = "main",

                            Value = JsonConvert.SerializeObject(volume),
                        };

                    }
                    else if (password == ShutdownApp.Cfg.Password || !ShutdownApp.Cfg.PasswordCheck || action == "handshake")
                    {
                        if (!ShutdownApp.Cfg.CheckMAC || GetHardwareAddress().Contains(hardwareAddress))
                        {

                            //_ = request.QueryString.Get("delay") == null ? delay : Convert.ToInt32(request.QueryString.Get("delay"));
                            //if (action.StartsWith("alexstar")) responseMode = "json";
                            switch (action)
                            {
                                case "alexstar_input":
                                    /*string sr = "";
                                    foreach (var s in request.QueryString.AllKeys)
                                    {
                                       sr += s + " - " + request.QueryString.Get(s) + "\n";
                                    }
                                    message = sr;*/
                                    //MessageBox.Show(sr);
                                    args = new()
                                    {
                                        Success = true,
                                        Alert_Type = "success",
                                        TemplateName = "alert",
                                        Value =  request.QueryString.Get("value") == "{value}" ? AlexstarHook.GetCurentInput() : request.QueryString.Get("value")

                                    };

                                    actionType = AlexstarHook.GetCommandByInput(request.QueryString.Get("value"));
                                   
                                    break;
                                case "alexstar_mute":
                                    /*string sr = "";
                                    foreach (var s in request.QueryString.AllKeys)
                                    {
                                        sr += s + " - " + request.QueryString.Get(s) + "\n";
                                    }
                                    message = sr;*/
                                    //MessageBox.Show(sr);
                                    args = new()
                                    {
                                        Success = true,
                                        Alert_Type = "success",
                                        TemplateName = "alert",
                                        //Value = AudioManager.GetMasterVolumeMute()

                                    };
                                    try
                                    {
                                        var mute = request.QueryString.Get("value");
                                    if (mute != null && mute != "{value}")
                                    {
                                        
                                        AudioManager.SetMasterVolumeMute(Convert.ToBoolean(Convert.ToInt32(mute)));
                                    }
                                    
                                    }
                                    catch (Exception)
                                    {
                                        args.Success = false;
                                        args.Alert_Type = "danger";
                                        args.Text = "Invalid argument.";
                                        args.ErrorCode = -1;
                                    }
                                    actionType = ShutdownTask.TaskType.None;
                                    args.Value = AudioManager.GetMasterVolumeMute();
                                    break;
                                case "alexstar_volume":
                                    /*string sr = "";
                                    foreach (var s in request.QueryString.AllKeys)
                                    {
                                        sr += s + " - " + request.QueryString.Get(s) + "\n";
                                    }
                                    message = sr;*/
                                    //MessageBox.Show(sr);
                                    args = new()
                                    {
                                        Success = true,
                                        Alert_Type = "success",
                                        TemplateName = "alert",

                                        //Value = Convert.ToInt32(AudioManager.GetMasterVolume())

                                    };
                                    try
                                    {
                                        var vol = request.QueryString.Get("value");
                                        if (vol != null && vol != "{value}" && vol != "")
                                        {
                                            //MessageBox.Show(vol);
                                            AudioManager.SetMasterVolume(Convert.ToSingle(vol));
                                        }

                                    }
                                    catch (Exception)
                                    {
                                        args.Success = false;
                                        args.Alert_Type = "danger";
                                        args.Text = "Error change volume. Invalid argument. ";
                                        args.ErrorCode = -1;
                                    }

                                    args.Value = Convert.ToInt32(AudioManager.GetMasterVolume());
                                    args.Text += "Current volume: " + AudioManager.GetMasterVolume();



                                    actionType = ShutdownTask.TaskType.None;
                                    break;
                                case "alexstar_channel":
                                    /*string sr = "";
                                    foreach (var s in request.QueryString.AllKeys)
                                    {
                                        sr += s + " - " + request.QueryString.Get(s) + "\n";
                                    }
                                    message = sr;*/
                                    //MessageBox.Show(sr);
                                    args = new()
                                    {
                                        Success = true,
                                        Alert_Type = "success",
                                        TemplateName = "alert",
                                        Value = AlexstarHook.GetCurrentChannel()

                                    };

                                    var channel = request.QueryString.Get("value");
                                    if (channel != null && channel != "{value}")
                                    {
                                        var k = ShutdownApp.Cfg.AlexStar.Channels[channel].Keys;
                                        foreach (var r in k)
                                        {
                                            //MessageBox.Show(r + " " + ShutdownApp.Cfg.AlexStar.Channels[channel][r]);
                                            if (r == "run")
                                            {
                                                Process.Start(ShutdownApp.Cfg.AlexStar.Channels[channel][r]);
                                            }
                                        }
                                        
                                        //AlexstarHook.SetCurrentChannel(channel);                                                                                
                                    }
                                    actionType = ShutdownTask.TaskType.None;
                                    break;
                                case "lock":
                                    args = new()
                                    {
                                        Text = ShutdownApp.Translation.Lang.Strings.LockScreen,
                                        Button = "<button type='button' class='btn btn-primary'onclick='window.location.replace(`/" + password_arg + "`)'>" + ShutdownApp.Translation.Lang.Strings.Back + "</button>",
                                        Alert_Type = "success",
                                        Success = true,
                                        TemplateName = "alert"
                                    };
                                    actionType = ShutdownTask.TaskType.Lock;
                                    break;
                                case "unlock":
                                    args = new()
                                    {
                                        Text = ShutdownApp.Translation.Lang.Strings.UnlockScreen,
                                        Button = "<button type='button' class='btn btn-primary'onclick='window.location.replace(`/" + password_arg + "`)'>" + ShutdownApp.Translation.Lang.Strings.Back + "</button>",
                                        Alert_Type = "success",
                                        Success = true,
                                        TemplateName = "alert"
                                    };
                                    actionType = ShutdownTask.TaskType.Unlock;
                                    break;
                                case "reboot":
                                    args = new()
                                    {
                                        Text = ShutdownApp.Translation.Lang.Strings.RebootPC,
                                        Button = "<button type='button' class='btn btn-primary'onclick='shutdown_action(`cancel`, `" + password + "`)'>" + ShutdownApp.Translation.Lang.Strings.CancelTasks + "</button>",
                                        Alert_Type = "success",
                                        Success = true,
                                        TemplateName = "alert"
                                    };
                                    actionType = ShutdownTask.TaskType.RebootPC;
                                    break;
                                case "shutdown":
                                    args = new()
                                    {
                                        Text = ShutdownApp.Translation.Lang.Strings.ShutdownPC,
                                        Button = "<button type='button' class='btn btn-primary'onclick='shutdown_action(`cancel`, `" + password + "`)'>" + ShutdownApp.Translation.Lang.Strings.CancelTasks + "</button>",
                                        Alert_Type = "success",
                                        Success = true,
                                        TemplateName = "alert"
                                    };
                                    actionType = ShutdownTask.TaskType.ShutdownPC;
                                    break;
                                case "sleep":
                                    args = new()
                                    {
                                        Text = ShutdownApp.Translation.Lang.Strings.Sleep,
                                        Button = "<!--<button type='button' class='btn btn-primary'onclick='shutdown_action(`cancel`, `" + password + "`)'>" + ShutdownApp.Translation.Lang.Strings.CancelTasks + "</button>",
                                        Alert_Type = "success",
                                        Success = true,
                                        TemplateName = "alert"
                                    };
                                    actionType = ShutdownTask.TaskType.Sleep;
                                    break;
                                case "hibernate":
                                    args = new()
                                    {
                                        Text = ShutdownApp.Translation.Lang.Strings.Hibernation,
                                        Button = "<!--<button type='button' class='btn btn-primary'onclick='shutdown_action(`cancel`, `" + password + "`)'>" + ShutdownApp.Translation.Lang.Strings.CancelTasks + "</button>-->",
                                        Alert_Type = "success",
                                        Success = true,
                                        TemplateName = "alert"
                                    };
                                    actionType = ShutdownTask.TaskType.Hibernaiton;
                                    break;
                                case "cancel":
                                    args = new()
                                    {
                                        Text = ShutdownApp.Translation.Lang.Strings.CancelTasks,
                                        Password = password_arg,
                                        Success = true,
                                        TemplateName = "redirect"
                                    };
                                    actionType = ShutdownTask.TaskType.Cancel;
                                    break;
                                case "notification":
                                    args = new()
                                    {
                                        Text = ShutdownApp.Translation.Lang.Strings.Notification,
                                        Password = password_arg,
                                        Alert_Type = "success",
                                        Success = true,
                                        TemplateName = "redirect"
                                    };
                                    actionType = ShutdownTask.TaskType.Notification;
                                    break;
                                case "screenoff":
                                    args = new()
                                    {
                                        Text = ShutdownApp.Translation.Lang.Strings.CommandDone,
                                        Alert_Type = "success",
                                        Success = true,
                                        TemplateName = "alert"
                                    };
                                    actionType = ShutdownTask.TaskType.ScreenOff;
                                    break;
                                case "screenshot":
                                    args = new()
                                    {
                                        Text = ShutdownApp.Translation.Lang.Strings.CommandDone,
                                        Alert_Type = "success",
                                        Success = true,
                                        TemplateName = "alert"
                                    };
                                    actionType = ShutdownTask.TaskType.Screenshot;
                                    responseMode = "image_png";
                                    break;
                                case "screenon":
                                    args = new()
                                    {
                                        Text = ShutdownApp.Translation.Lang.Strings.CommandDone,
                                        Alert_Type = "success",
                                        Success = true,
                                        TemplateName = "alert"
                                    };
                                    actionType = ShutdownTask.TaskType.ScreenOn;
                                    break;
                               
                                case "media_pause":
                                    ss = new(256);
                                    _ = ServerHelpers.GetWindowText(ServerHelpers.GetForegroundWindow(), ss, 256);
                                    args = new()
                                    {
                                        Success = true,
                                        Text = ss.ToString(),
                                        Password = password_arg,
                                        Alert_Type = "success",
                                        TemplateName = "redirect"
                                    };
                                    actionType = ShutdownTask.TaskType.MediaPause;
                                    break;
                                case "media_next":
                                    ss = new(256);
                                    _ = ServerHelpers.GetWindowText(ServerHelpers.GetForegroundWindow(), ss, 256);
                                    args = new()
                                    {
                                        Success = true,
                                        Text = ss.ToString(),
                                        Password = password_arg,
                                        Alert_Type = "success",
                                        TemplateName = "redirect"
                                    };
                                    actionType = ShutdownTask.TaskType.MediaNext;
                                    break;
                                case "media_prev":
                                    ss = new(256);
                                    _ = ServerHelpers.GetWindowText(ServerHelpers.GetForegroundWindow(), ss, 256);
                                    args = new()
                                    {
                                        Success = true,
                                        Text = ss.ToString(),
                                        Password = password_arg,
                                        Alert_Type = "success",
                                        TemplateName = "redirect"
                                    };
                                    actionType = ShutdownTask.TaskType.MediaPrev;
                                    break;
                                case "media_volume_mute":
                                    ss = new(256);
                                    _ = ServerHelpers.GetWindowText(ServerHelpers.GetForegroundWindow(), ss, 256);
                                    args = new()
                                    {
                                        Success = true,
                                        Text = ss.ToString(),
                                        Password = password_arg,
                                        Alert_Type = "success",
                                        TemplateName = "redirect"
                                    };
                                    actionType = ShutdownTask.TaskType.VolumeMute;
                                    break;
                                case "media_volume_up":
                                    ss = new(256);
                                    _ = ServerHelpers.GetWindowText(ServerHelpers.GetForegroundWindow(), ss, 256);
                                    args = new()
                                    {
                                        Success = true,
                                        Text = ss.ToString(),
                                        Password = password_arg,
                                        Alert_Type = "success",
                                        TemplateName = "redirect"
                                    };
                                    actionType = ShutdownTask.TaskType.VolumeUp;
                                    break;
                                case "media_volume_down":
                                    ss = new(256);
                                    _ = ServerHelpers.GetWindowText(ServerHelpers.GetForegroundWindow(), ss, 256);
                                    args = new()
                                    {
                                        Success = true,
                                        Text = ss.ToString(),
                                        Password = password_arg,
                                        Alert_Type = "success",
                                        TemplateName = "redirect"
                                    };
                                    actionType = ShutdownTask.TaskType.VolumeDown;
                                    break;
                                case "handshake":
                                    args = new()
                                    {
                                        Text = ShutdownApp.Translation.Lang.Strings.CommandDone,
                                        Success = true,
                                        Value = true,
                                        Alert_Type = "success",
                                        TemplateName = "alert"
                                    };
                                    responseMode = "json";
                                    actionType = ShutdownTask.TaskType.None;
                                    break;


                                default:
                                    args = new()
                                    {
                                        Text = ShutdownApp.Translation.Lang.Strings.ActionNotSet,
                                        Password = password_arg,
                                        Success = false,
                                        Button = "<button type='button' class='btn btn-primary'onclick='window.location.replace(`/" + password_arg + "`)'>" + ShutdownApp.Translation.Lang.Strings.Home + "</button>",
                                        Alert_Type = "danger",
                                        TemplateName = "alert"
                                    };
                                    actionType = ShutdownTask.TaskType.None;
                                    break;
                            }
                            if (actionType != ShutdownTask.TaskType.None)
                            {
                                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(time).ToLocalTime();
                                AddTask(actionType, dateTimeOffset.DateTime, message);
                            }


                        }
                        else
                        {
                            args = new()
                            {
                                Text = ShutdownApp.Translation.Lang.Strings.WrongMac,
                                Password = password_arg,
                                Success = false,
                                Button = "<button type='button' class='btn btn-primary'onclick='window.location.replace(`/" + password_arg + "`)'>" + ShutdownApp.Translation.Lang.Strings.Home + "</button>",
                                Alert_Type = "danger",
                                TemplateName = "alert"
                            };
                        }


                    }
                    else
                    {
                        args = new()
                        {
                            Text = ShutdownApp.Translation.Lang.Strings.WrongPassword,
                            Success = false,
                            Button = "<div class='input-group mb-3'><input type='password' class='form-control' placeholder='" + ShutdownApp.Translation.Lang.Strings.EnterPassword + "' aria-label='" + ShutdownApp.Translation.Lang.Strings.EnterPassword + "' id='password' aria-describedby='button-addon2'><button class='btn btn-outline-secondary' onclick='enter_password()' type='button' id='button-addon2'>" + ShutdownApp.Translation.Lang.Strings.SendButton + "</button></div>",
                            Script = "<script type='text/javascript'>function enter_password(){input = document.getElementById('password');  window.location.replace('/?password='+input.value); }</script>",
                            Alert_Type = "danger",
                            TemplateName = "alert"
                        };
                    }



                    answer = ParseTemplate(args, responseMode);
                    string responseString = answer;
                    byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                    response.Headers = new WebHeaderCollection
                    {
                        "Content-Type:text/html;charset=UTF-8"
                    };
                    if (request.RawUrl.EndsWith(".ico") || request.RawUrl.EndsWith(".png"))
                    {
                        string workpath = ShutdownApp.Cfg.WorkPath;
                        buffer = File.ReadAllBytes(Path.Combine(workpath, @"UI/images" + request.RawUrl));
                        var header = "Content-Type:image/" + request.RawUrl.Split('.')[^1] + ";charset=UTF-8";
                        response.Headers = new WebHeaderCollection
                        {

                            header
                        };
                    }
                    if (responseMode == "image_png")
                    {
                        buffer = File.ReadAllBytes(Screenshot.Save());
                        var header = "Content-Type:image/png;charset=UTF-8";
                        response.Headers = new WebHeaderCollection
                        {

                            header
                        };
                    }

                    
                    response.ContentLength64 = buffer.Length;
                    try 
                    {
                        Stream output = response.OutputStream;
                        output.Write(buffer, 0, buffer.Length);
                        
                    } 
                    catch (Exception)
                    {  
                        
                    }
                    finally
                    {
                        response.Close();
                    }
                   




                }



            }
        }

        private static string ParseErrorTemplate(int code, string message, string trace, string source)
        {
            string html = "<!DOCTYPE html>\r\n<html>\r\n    <head>\r\n        <meta http-equiv = 'Content-Type' content = 'text/html; charset=utf-8'> \r\n        <link href='https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/css/bootstrap.min.css' rel='stylesheet' integrity='sha384-0evHe/X+R7YkIZDRvuzKMRqM+OrBnVFBL6DOitfPri4tjfHxaWutUpFmBp4vmVor' crossorigin='anonymous'>\r\n        <title> PCShutdown</title>\r\n    </head>\r\n    <body>\r\n    <div class=\"container\">\r\n        {{body}}\r\n    </div>\r\n\r\n    <script src='https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/js/bootstrap.bundle.min.js' integrity='sha384-pprn3073KE6tl6bjs2QrFaJGz5/SUsLqktiwsUTF55Jfv3qYSDhgCecCxMW52nD2' crossorigin='anonymous'></script>\r\n    </body>\r\n    </html> ";

            string starcktrace = "<p>\r\n  <a class=\"btn btn-primary\" data-bs-toggle=\"collapse\" href=\"#stacktrace\" role=\"button\" aria-expanded=\"false\" aria-controls=\"stacktrace\">\r\n    Show StackTrace &raquo;\r\n  </a>\r\n  \r\n</p>\r\n<div class=\"collapse\" id=\"stacktrace\">\r\n  <div class=\"card card-body\">\r\n  " + trace + " \r\n <div class=\"alert alert-primary d-flex align-items-center\" role=\"alert\">\r\n  <div>\r\n    <h4 class=\"alert-heading\">Source:</h4><p>  " + source + "\r\n  </p></div>\r\n</div> </div>\r\n</div>";
            string parsed = string.Format("<h1 class=\"display-4\">Error {0}</h1><hr><div class=\"alert alert-danger\" role=\"alert\">\r\n {1}\r\n</div>{2}", code, message, starcktrace);



            html = html.Replace("{{body}}", parsed);
            return html;
        }

        public static IDictionary<string, object> GetValues(object obj)
        {
            return obj
                    .GetType()
                    .GetProperties()
                    .ToDictionary(p => p.Name.ToLower(), p => p.GetValue(obj)) ;
        }

        private static string ParseTemplate(Args args, string mode = default)
        {
            try
            {
                string workpath = ShutdownApp.Cfg.WorkPath;

                string base_template = File.ReadAllText(Path.Combine(workpath, @"UI/base.template"));
                string template = File.ReadAllText(Path.Combine(workpath, @"UI/" + args.TemplateName + ".template"));
               

                var kvp = GetValues(args);
                foreach ( var arg in kvp) {
                    if (arg.Value != null)
                    {
                        template = template.Replace("{{" + arg.Key.ToLower() + "}}", arg.Value.ToString());
                    }
                    
                }

              
                Type t = ShutdownApp.Translation.Lang.Strings.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (var prop in props)
                {
                    template = template.Replace("{{strings."+prop.Name+"}}", prop.GetValue(ShutdownApp.Translation.Lang.Strings).ToString());//  
                }



                //Regex rg = new Regex("/{/{/w+/}/}");
                template = TemplaterRegex().Replace(template, "");
                Console.Write(mode);
                var serialier_settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                if (mode == "json")
                {
                    args.Button = null;
                    args.Script = null;
                    args.TemplateName = null;
                }

                var rendered_template = base_template.Replace("{{body}}", template);
                return mode switch
                {
                    "text" => args.Text,
                    "json" => JsonConvert.SerializeObject(args, serialier_settings).ToLower(),//"{\"" + args[1].Item1 + "\":\"" + args[1].Item2 + "\"}",
                    _ => rendered_template,
                }; 
            }
            catch (Exception exception)
            {
                return ParseErrorTemplate(404, exception.Message, exception.StackTrace, exception.Source);
            }

        }

        private static async void ShowNotification(string message, string attrib="")
        {
            await Task.Run(() => { ShutdownApp.ShowToast(message, attrib); });

        }



        public static void ExecCommand(ShutdownTask.TaskType command, int delay = 0, string message = "")
        {
            var ago = DateTime.Now.AddSeconds(delay); 
            string args = default;
            string baloon_text = default;
            string attrib_text = default;
            string pin;
            switch (command)
            {

                case ShutdownTask.TaskType.Cancel:
                    baloon_text = ShutdownApp.Translation.Lang.Strings.QueueCleared;
                    attrib_text = ShutdownApp.Translation.Lang.Strings.TasksCanceled;
                    RemoveAllTasks();
                    args = "-a";
                    break;
                case ShutdownTask.TaskType.RebootPC:
                    baloon_text = ShutdownApp.Translation.Lang.Strings.WillRebooted + " " + ago;
                    args = "-r -t " + delay + " -c \"PCShutdown: " + ShutdownApp.Translation.Lang.Strings.WillRebooted + " " + ago + "\"";
                    break;
                case ShutdownTask.TaskType.Lock:
                    //baloon_text =  message;
                    pin = ShutdownApp.Cfg.UnlockPin.ToString();
                    if (message != "")
                        pin = message;
                    attrib_text = ShutdownApp.Translation.Lang.Strings.LockScreen;
                    ShutdownApp.Forms.ShowForm(typeof(Forms.ScreenLockerForm), pin);
                    break;
                case ShutdownTask.TaskType.Unlock:
                    baloon_text = message;
                    pin = ShutdownApp.Cfg.UnlockPin.ToString();

                    if (message != "" && message != null)
                        pin = message;
                    attrib_text = ShutdownApp.Translation.Lang.Strings.LockScreen;
                    
                    ShutdownApp.Forms.CloseForm(typeof(Forms.ScreenLockerForm), pin);
                    break;
                case ShutdownTask.TaskType.Sleep:
                    baloon_text = message;
                    attrib_text = ShutdownApp.Translation.Lang.Strings.Sleep;
                    ServerHelpers.SetSuspendState(false);
                    break;
                case ShutdownTask.TaskType.Hibernaiton:
                    baloon_text = message;
                    attrib_text = ShutdownApp.Translation.Lang.Strings.Hibernation;
                    ServerHelpers.SetSuspendState(true);
                    break;
                case ShutdownTask.TaskType.ShutdownPC:
                    baloon_text = ShutdownApp.Translation.Lang.Strings.WillShutdowned + " " + ago;
                    attrib_text = ShutdownApp.Translation.Lang.Strings.RebootPC;
                    args = " -s -t " + delay + " -c \"PCShutdown: " + ShutdownApp.Translation.Lang.Strings.WillShutdowned + " " + ago + " \"";
                    break;
                case ShutdownTask.TaskType.Notification:
                    attrib_text = ShutdownApp.Translation.Lang.Strings.Notification;
                    ShowNotification(message, attrib_text);
                    //baloon_text = default;
                    break;
                case ShutdownTask.TaskType.ScreenOff:
                    ServerHelpers.TurnOffDisplay();
                    break;
                case ShutdownTask.TaskType.ScreenOn:
                    ServerHelpers.TurnOnDisplay();
                    break;
                case ShutdownTask.TaskType.MediaPause:
                    baloon_text = message;

                    ServerHelpers.MediaKeyEmulate(Keys.MediaPlayPause);
                    break;
                case ShutdownTask.TaskType.MediaNext:
                    baloon_text = message;

                    ServerHelpers.MediaKeyEmulate(Keys.MediaNextTrack);
                    break;
                case ShutdownTask.TaskType.MediaPrev:
                    baloon_text = message;

                    ServerHelpers.MediaKeyEmulate(Keys.MediaPreviousTrack);
                    break;
                case ShutdownTask.TaskType.VolumeMute:
                    baloon_text = message;

                    ServerHelpers.MediaKeyEmulate(Keys.VolumeMute);
                    break;
                case ShutdownTask.TaskType.VolumeUp:
                    baloon_text = message;

                    ServerHelpers.MediaKeyEmulate(Keys.VolumeUp);
                    break;
                case ShutdownTask.TaskType.VolumeDown:
                    baloon_text = message;

                    ServerHelpers.MediaKeyEmulate(Keys.VolumeDown);
                    break;
                case ShutdownTask.TaskType.Screenshot:
                    baloon_text = message;
                    
                    break;
                default:
                    baloon_text = ShutdownApp.Translation.Lang.Strings.WrongCommand;
                    args = default;
                    break;
            }
            if (baloon_text != default) ShutdownApp.ShowToast(baloon_text, attrib_text);
            if (args != default)
            {
                ProcessStartInfo startInfo = new("netsh")
                {
                    UseShellExecute = true,
                    Verb = "runas",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "shutdown",
                    Arguments = args,
                    CreateNoWindow = true
                };
                Process.Start(startInfo);
            }
        }


        public static List<ServerNetworkInterface> GetNetworkInterfaces()
        {

            List<ServerNetworkInterface> output = new();

            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                PhysicalAddress if_mac;
                string if_name;
                string if_desc;

                if (item.OperationalStatus == OperationalStatus.Up && (item.NetworkInterfaceType == NetworkInterfaceType.Ethernet || item.NetworkInterfaceType == NetworkInterfaceType.Wireless80211))
                {
                    if_name = item.Name;
                    if_mac = item.GetPhysicalAddress();
                    if_desc = item.Description;
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(ip.Address))
                        {
                            output.Add(new ServerNetworkInterface(if_name, ip.Address, if_mac, if_desc));
                        }
                    }
                }
            }
            return output;
        }


        public static List<string> GetHardwareAddress()
        {
            List<string> output = new();
            foreach (ServerNetworkInterface item in GetNetworkInterfaces())
            {
                output.Add(item.HardwareAddress.ToString());
            }
            if (output.Count != 0) { return output; }
            else
            {
                output.Add(ShutdownApp.Translation.Lang.Strings.NoActiveInterface);
                return output;
            }
        }

        public static ServerNetworkInterface GetInterfaceByIP(string ip)
        {

            return GetNetworkInterfaces().Find(x => x.IPv4.ToString() == ip);
        }
        public static List<string> GetLocalIPv4()
        {
            List<string> output = new();
            foreach (ServerNetworkInterface item in GetNetworkInterfaces())
            {
                output.Add(item.IPv4.ToString());
            }
            if (output.Count != 0) { return output; }
            else
            {
                output.Add(ShutdownApp.Translation.Lang.Strings.NoActiveInterface);
                return output;
            }
        }

        [GeneratedRegex("\\{\\{(\\w+)\\}\\}")]
        private static partial Regex TemplaterRegex();
    }

    class ServerNetworkInterface
    {
        public string Name { get; set; }
        public IPAddress IPv4 { get; set; }
        public PhysicalAddress HardwareAddress { get; set; }
        public string Description { get; set; }
        public ServerNetworkInterface(string name, IPAddress ipv4, PhysicalAddress macAddress, string description)
        {
            Name = name;
            IPv4 = ipv4;
            HardwareAddress = macAddress;
            Description = description;
        }

    }
}

