using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCShutdown.Classes
{

    class Server
    {



        public static int localPort = Properties.Settings.Default.ServerPort;
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
                int delay = Properties.Settings.Default.Delay;



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
                    List<Tuple<string, string>> args = new();
                    if (action == null && (password == Properties.Settings.Default.Password || !Properties.Settings.Default.PasswordCheck))
                    {
                        args.Clear();
                        args.Add(new("text", ShutdownApp.Translation.Lang.Strings.ActionNotSet));
                        args.Add(new("password", password));
                        answer = ParseTemplate("main", args, responseMode);
                    }
                    else if (password == Properties.Settings.Default.Password || !Properties.Settings.Default.PasswordCheck || action == "handshake")
                    {
                        if (!Properties.Settings.Default.CheckMAC || GetHardwareAddress().Contains(hardwareAddress))
                        {

                            _ = request.QueryString.Get("delay") == null ? delay : Convert.ToInt32(request.QueryString.Get("delay"));
                            switch (action)
                            {

                                case "lock":
                                    args.Clear();
                                    args.Add(new("text", ShutdownApp.Translation.Lang.Strings.LockScreen));
                                    args.Add(new("button", "<button type='button' class='btn btn-primary'onclick='window.location.replace(`/" + password_arg + "`)'>" + ShutdownApp.Translation.Lang.Strings.Back + "</button>"));
                                    args.Add(new("alert_type", "success"));
                                    answer = ParseTemplate("alert", args, responseMode);
                                    actionType = ShutdownTask.TaskType.Lock;
                                    break;
                                case "unlock":
                                    args.Clear();
                                    args.Add(new("text", ShutdownApp.Translation.Lang.Strings.LockScreen));
                                    args.Add(new("button", "<button type='button' class='btn btn-primary'onclick='window.location.replace(`/" + password_arg + "`)'>" + ShutdownApp.Translation.Lang.Strings.Back + "</button>"));
                                    args.Add(new("alert_type", "success"));
                                    answer = ParseTemplate("alert", args, responseMode);
                                    actionType = ShutdownTask.TaskType.Unlock;
                                    break;
                                case "reboot":
                                    args.Clear();
                                    args.Add(new("text", ShutdownApp.Translation.Lang.Strings.RebootPC));
                                    args.Add(new("button", "<button type='button' class='btn btn-primary'onclick='shutdown_action(`cancel`, `" + password + "`)'>" + ShutdownApp.Translation.Lang.Strings.CancelTasks + "</button>"));
                                    args.Add(new("alert_type", "success"));
                                    answer = ParseTemplate("alert", args, responseMode);
                                    actionType = ShutdownTask.TaskType.RebootPC;
                                    break;
                                case "shutdown":
                                    args.Clear();
                                    args.Add(new("text", ShutdownApp.Translation.Lang.Strings.ShutdownPC));
                                    args.Add(new("button", "<button type='button' class='btn btn-primary'onclick='shutdown_action(`cancel`, `" + password + "`)'>" + ShutdownApp.Translation.Lang.Strings.CancelTasks + "</button>"));
                                    args.Add(new("alert_type", "success"));
                                    answer = ParseTemplate("alert", args, responseMode);
                                    actionType = ShutdownTask.TaskType.ShutdownPC;
                                    break;
                                case "sleep":
                                    args.Clear();
                                    args.Add(new("text", ShutdownApp.Translation.Lang.Strings.Sleep));
                                    args.Add(new("button", "<!--<button type='button' class='btn btn-primary'onclick='shutdown_action(`cancel`, `" + password + "`)'>" + ShutdownApp.Translation.Lang.Strings.CancelTasks + "</button>"));
                                    args.Add(new("alert_type", "success"));
                                    answer = ParseTemplate("alert", args, responseMode);
                                    actionType = ShutdownTask.TaskType.Sleep;
                                    break;
                                case "hibernate":
                                    args.Clear();
                                    args.Add(new("text", ShutdownApp.Translation.Lang.Strings.Hibernation));
                                    args.Add(new("button", "<!--<button type='button' class='btn btn-primary'onclick='shutdown_action(`cancel`, `" + password + "`)'>" + ShutdownApp.Translation.Lang.Strings.CancelTasks + "</button>-->"));
                                    args.Add(new("alert_type", "success"));
                                    answer = ParseTemplate("alert", args, responseMode);
                                    actionType = ShutdownTask.TaskType.Hibernaiton;
                                    break;
                                case "cancel":
                                    args.Clear();
                                    args.Add(new("text", ShutdownApp.Translation.Lang.Strings.CancelTasks));
                                    args.Add(new("password", password_arg));
                                    answer = ParseTemplate("redirect", args, responseMode);
                                    actionType = ShutdownTask.TaskType.Cancel;
                                    break;
                                case "notification":
                                    args.Clear();
                                    args.Add(new("text", ShutdownApp.Translation.Lang.Strings.Notification));
                                    args.Add(new("password", password_arg));
                                    args.Add(new("alert_type", "success"));
                                    answer = ParseTemplate("redirect", args, responseMode);
                                    actionType = ShutdownTask.TaskType.Notification;
                                    break;
                                case "screenoff":
                                    args.Clear();
                                    args.Add(new("text", ShutdownApp.Translation.Lang.Strings.CommandDone));
                                    args.Add(new("alert_type", "success"));
                                    answer = ParseTemplate("alert", args, responseMode);
                                    actionType = ShutdownTask.TaskType.ScreenOff;
                                    break;
                                case "screenon":
                                    args.Clear();
                                    args.Add(new("text", ShutdownApp.Translation.Lang.Strings.CommandDone));
                                    args.Add(new("alert_type", "success"));
                                    answer = ParseTemplate("alert", args, responseMode);
                                    actionType = ShutdownTask.TaskType.ScreenOn;
                                    break;
                                case "press_space":
                                    args.Clear();
                                    StringBuilder ss = new(256);
                                    _ = ServerHelpers.GetWindowText(ServerHelpers.GetForegroundWindow(), ss, 256);
                                    args.Add(new("text", ss.ToString()));
                                    args.Add(new("alert_type", "success"));
                                    answer = ParseTemplate("alert", args, responseMode);
                                    actionType = ShutdownTask.TaskType.PressSpace;
                                    break;
                                case "handshake":
                                    args.Clear();
                                    args.Add(new("text", ShutdownApp.Translation.Lang.Strings.CommandDone));
                                    args.Add(new("alert_type", "success"));
                                    answer = ParseTemplate("alert", args, responseMode);
                                    actionType = ShutdownTask.TaskType.None;
                                    break;


                                default:
                                    args.Clear();
                                    args.Add(new("text", ShutdownApp.Translation.Lang.Strings.ActionNotSet));
                                    args.Add(new("password", password_arg));
                                    args.Add(new("button", "<button type='button' class='btn btn-primary'onclick='window.location.replace(`/" + password_arg + "`)'>" + ShutdownApp.Translation.Lang.Strings.Home + "</button>"));
                                    args.Add(new("alert_type", "danger"));
                                    answer = ParseTemplate("alert", args, responseMode);
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
                            args.Clear();
                            args.Add(new Tuple<string, string>("text", ShutdownApp.Translation.Lang.Strings.WrongMac));
                            args.Add(new Tuple<string, string>("password", password_arg));
                            args.Add(new Tuple<string, string>("button", "<button type='button' class='btn btn-primary'onclick='window.location.replace(`/" + password_arg + "`)'>"+ ShutdownApp.Translation.Lang.Strings.Home+"</button>"));
                            args.Add(new Tuple<string, string>("alert_type", "danger"));
                            answer = ParseTemplate("alert", args, responseMode);
                        }


                    }
                    else
                    {
                        args.Clear();
                        args.Add(new Tuple<string, string>("text", ShutdownApp.Translation.Lang.Strings.WrongPassword));
                        args.Add(new Tuple<string, string>("button", "<div class='input-group mb-3'><input type='password' class='form-control' placeholder='"+ ShutdownApp.Translation.Lang.Strings.EnterPassword+ "' aria-label='"+ ShutdownApp.Translation.Lang.Strings.EnterPassword+ "' id='password' aria-describedby='button-addon2'><button class='btn btn-outline-secondary' onclick='enter_password()' type='button' id='button-addon2'>"+ ShutdownApp.Translation.Lang.Strings.SendButton+ "</button></div>"));
                        args.Add(new Tuple<string, string>("script", "<script type='text/javascript'>function enter_password(){input = document.getElementById('password');  window.location.replace('/?password='+input.value); }</script>"));
                        args.Add(new Tuple<string, string>("alert_type", "danger"));
                        answer = ParseTemplate("alert", args, responseMode);
                    }


                    string responseString = answer;
                    byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                    response.ContentLength64 = buffer.Length;
                    response.Headers = new WebHeaderCollection
                    {
                        "Content-Type:text/html;charset=UTF-8"
                    };
                    Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    output.Close();




                }



            }
        }

        private static string ParseErrorTemplate(int code, string message, string trace, string source, params object[] args)
        {
            string html = "<!DOCTYPE html>\r\n<html>\r\n    <head>\r\n        <meta http-equiv = 'Content-Type' content = 'text/html; charset=utf-8'> \r\n        <link href='https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/css/bootstrap.min.css' rel='stylesheet' integrity='sha384-0evHe/X+R7YkIZDRvuzKMRqM+OrBnVFBL6DOitfPri4tjfHxaWutUpFmBp4vmVor' crossorigin='anonymous'>\r\n        <title> PCShutdown</title>\r\n    </head>\r\n    <body>\r\n    <div class=\"container\">\r\n        {{body}}\r\n    </div>\r\n\r\n    <script src='https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/js/bootstrap.bundle.min.js' integrity='sha384-pprn3073KE6tl6bjs2QrFaJGz5/SUsLqktiwsUTF55Jfv3qYSDhgCecCxMW52nD2' crossorigin='anonymous'></script>\r\n    </body>\r\n    </html> ";

            string starcktrace = "<p>\r\n  <a class=\"btn btn-primary\" data-bs-toggle=\"collapse\" href=\"#stacktrace\" role=\"button\" aria-expanded=\"false\" aria-controls=\"stacktrace\">\r\n    Show StackTrace &raquo;\r\n  </a>\r\n  \r\n</p>\r\n<div class=\"collapse\" id=\"stacktrace\">\r\n  <div class=\"card card-body\">\r\n  " + trace + " \r\n <div class=\"alert alert-primary d-flex align-items-center\" role=\"alert\">\r\n  <div>\r\n    <h4 class=\"alert-heading\">Source:</h4><p>  " + source + "\r\n  </p></div>\r\n</div> </div>\r\n</div>";
            string parsed = string.Format("<h1 class=\"display-4\">Error {0}</h1><hr><div class=\"alert alert-danger\" role=\"alert\">\r\n {1}\r\n</div>{2}", code, message, starcktrace);



            html = html.Replace("{{body}}", parsed);
            return html;
        }

        private static string ParseTemplate(string name, List<Tuple<string, string>> args, string mode = default)
        {
            try
            {
                string workpath = Properties.Settings.Default.WorkPath;

                string base_template = File.ReadAllText(Path.Combine(workpath, @"UI/base.template"));
                string template = File.ReadAllText(Path.Combine(workpath, @"UI/" + name + ".template"));
                //string template = "";

                foreach (Tuple<string, string> arg in args)
                {
                    template = template.Replace("{{" + arg.Item1 + "}}", arg.Item2);
                }
                Type t = ShutdownApp.Translation.Lang.Strings.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (var prop in props)
                {
                    template = template.Replace("{{strings."+prop.Name+"}}", prop.GetValue(ShutdownApp.Translation.Lang.Strings).ToString());//  
                }

                //Regex rg = new Regex("/{/{/w+/}/}");
                template = Regex.Replace(template, @"\{\{(\w+)\}\}", "");
                Console.Write(mode);
                return mode switch
                {
                    "text" => args[0].Item2,
                    "json" => "{\"" + args[0].Item1 + "\":\"" + args[0].Item2 + "\"}",
                    "html" => base_template.Replace("{{body}}", template),
                    _ => base_template.Replace("{{body}}", template),
                };
            }
            catch (Exception exception)
            {
                return ParseErrorTemplate(404, exception.Message, exception.StackTrace, exception.Source);
            }

        }

        private static async void ShowNotification(string message, string attrib)
        {
            await Task.Run(() => { ShutdownApp.ShowToast(message, attrib); });

        }



        public static void ExecCommand(ShutdownTask.TaskType command, int delay = 0, string message = "")
        {
            var ago = DateTime.Now.AddSeconds(delay); 
            string args = default;
            string baloon_text = default;
            string attrib_text = default;
            string pin = default;
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
                    pin = Properties.Settings.Default.UnlockPin;
                    if (message != "")
                        pin = message;
                    attrib_text = ShutdownApp.Translation.Lang.Strings.LockScreen;
                    ShutdownApp.Forms.ShowForm(typeof(Forms.ScreenLockerForm), pin);
                    break;
                case ShutdownTask.TaskType.Unlock:
                    baloon_text = message;
                    pin = Properties.Settings.Default.UnlockPin;
                    if (message != "")
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
                case ShutdownTask.TaskType.PressSpace:
                    baloon_text = message;
                    
                    ServerHelpers.EmulateKeyClick(Keys.Space, false);  
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
                PhysicalAddress if_mac = default;
                string if_name = default;
                string if_desc = default;

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

