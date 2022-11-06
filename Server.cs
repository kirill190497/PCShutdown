using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace PCShutdown
{
    class Server
    {
        public static int localPort = 8888;

        [DllImport("user32.dll")]
        static extern bool LockWorkStation();
        [DllImport("PowrProf.dll")]
        static extern bool SetSuspendState(bool bHibernate);




        public static void Start()
        {
            try
            {
                // Получаем данные, необходимые для соединения





                Thread tRec = new Thread(new ThreadStart(Listen));
                tRec.Start();
                //Task.Run(Listen);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);

                ShutdownApp.ShowBallon("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message, ToolTipIcon.Error);
            }
        }





        private static async void Listen()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://+:" + localPort + "/");
            bool running = true;

            try
            {
                listener.Start();
            }
            catch (HttpListenerException e)
            {
                MessageBox.Show("Ошибка запуска! \n" + e.Message, "Ошибка");
                running = false;
                Application.Exit();
            }

            Console.WriteLine("Ожидание подключений...");

            while (running)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                string password;
                string action;
                string answer;
                string responseMode;
                int delay = Properties.Settings.Default.Delay;



                if (request.HttpMethod == "GET")
                {
                    password = request.QueryString.Get("password");
                    action = request.QueryString.Get("action");
                    responseMode = request.QueryString.Get("response");
                    string password_arg = password != null ? "?password=" + password : "";
                    List<Tuple<string, string>> args = new List<Tuple<string, string>>();
                    if (action == null && (password == Properties.Settings.Default.Password || !Properties.Settings.Default.PasswordCheck))
                    {
                        args.Clear();
                        args.Add(new Tuple<string, string>("text", "Не указано действие"));
                        args.Add(new Tuple<string, string>("password", password));
                        answer = ParseTemplate("main", args, responseMode);
                    }
                    else if ((password == Properties.Settings.Default.Password || !Properties.Settings.Default.PasswordCheck) || action == "handshake")
                    {
                        delay = request.QueryString.Get("delay") == null ? delay : Convert.ToInt32(request.QueryString.Get("delay"));
                        switch (action)
                        {
                            case "lock":
                                args.Clear();
                                args.Add(new Tuple<string, string>("text", "Блокировка" + responseMode));
                                args.Add(new Tuple<string, string>("button", "<button type='button' class='btn btn-primary'onclick='window.location.replace(`/" + password_arg + "`)'>Back</button>"));
                                answer = ParseTemplate("success", args, responseMode);
                                break;

                            case "reboot":
                                args.Clear();
                                args.Add(new Tuple<string, string>("text", "Перезагрузка"));
                                args.Add(new Tuple<string, string>("button", "<button type='button' class='btn btn-primary'onclick='shutdown_action(`cancel`, `" + password + "`)'>Отмена</button>"));
                                answer = ParseTemplate("success", args, responseMode);
                                break;
                            case "shutdown":
                                args.Clear();
                                args.Add(new Tuple<string, string>("text", "Выключение"));
                                args.Add(new Tuple<string, string>("button", "<button type='button' class='btn btn-primary'onclick='shutdown_action(`cancel`, `" + password + "`)'>Отмена</button>"));
                                answer = ParseTemplate("success", args, responseMode);
                                break;
                            case "sleep":
                                args.Clear();
                                args.Add(new Tuple<string, string>("text", "Спящий режим"));
                                args.Add(new Tuple<string, string>("button", "<!--<button type='button' class='btn btn-primary'onclick='shutdown_action(`cancel`, `" + password + "`)'>Отмена</button>-->"));
                                answer = ParseTemplate("success", args, responseMode);
                                break;
                            case "hibernate":
                                args.Clear();
                                args.Add(new Tuple<string, string>("text", "Гибернация"));
                                args.Add(new Tuple<string, string>("button", "<!--<button type='button' class='btn btn-primary'onclick='shutdown_action(`cancel`, `" + password + "`)'>Отмена</button>-->"));
                                answer = ParseTemplate("success", args, responseMode);
                                break;
                            case "cancel":
                                args.Clear();
                                args.Add(new Tuple<string, string>("text", "Отмена действия"));
                                args.Add(new Tuple<string, string>("password", password_arg));
                                answer = ParseTemplate("redirect", args, responseMode);
                                break;
                                
                            case "handshake":
                                args.Clear();
                                args.Add(new Tuple<string, string>("text", "Okay")); 
                                answer = ParseTemplate("success", args, responseMode);
                                break;


                            default:
                                args.Clear();
                                args.Add(new Tuple<string, string>("text", "Действие не определено"));
                                args.Add(new Tuple<string, string>("password", password_arg));
                                args.Add(new Tuple<string, string>("button", "<button type='button' class='btn btn-primary'onclick='window.location.replace(`/" + password_arg + "`)'>Home</button>"));
                                answer = ParseTemplate("danger", args, responseMode);
                                break;
                        }
                        execCommand(action, delay);

                    }
                    else
                    {
                        args.Clear();
                        args.Add(new Tuple<string, string>("text", "Пароль не верный или не указан"));
                        args.Add(new Tuple<string, string>("button", "<div class='input-group mb-3'><input type='password' class='form-control' placeholder='Введите пароль' aria-label='Введите пароль' id='password' aria-describedby='button-addon2'><button class='btn btn-outline-secondary' onclick='enter_password()' type='button' id='button-addon2'>Вход</button></div>"));
                        args.Add(new Tuple<string, string>("script", "<script type='text/javascript'>function enter_password(){input = document.getElementById('password');  window.location.replace('/?password='+input.value); }</script>"));
                        answer = ParseTemplate("danger", args, responseMode);
                    }


                    string responseString = answer;
                    byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                    response.ContentLength64 = buffer.Length;
                    response.Headers = new WebHeaderCollection();
                    response.Headers.Add("Content-Type:text/html;charset=UTF-8");
                    Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    output.Close();




                }



            }
        }

        private static string ParseTemplate(string name, List<Tuple<string, string>> args, string mode=default)
        {
            string base_template = File.ReadAllText(@"UI/base.template");
            string template = File.ReadAllText(@"UI/" + name + ".template");
            //string template = "";
            
            foreach (Tuple<string, string> arg in args)
            {
                template = template.Replace("{{" + arg.Item1 + "}}", arg.Item2);
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


        public static void execCommand(string command, int delay = 0)
        {
            ProcessStartInfo startInfo = default;

            string args = default;
            string baloon_text = default;
            switch (command)
            {
                case "cancel":
                    baloon_text = "Отмена операции отключения/перезагрузки";
                    args = "-a";
                    break;
                case "reboot":
                    baloon_text = "Компьютер будет перезагружен через " + delay + " секунд";
                    args = "-r -t " + delay + " -c \"Компьютер будет перезагружен через " + delay + " секунд\"";
                    break;
                case "lock":
                    baloon_text = "Блокировка";
                    LockWorkStation();
                    break;
                case "sleep":
                    baloon_text = "Спящий режим";
                    SetSuspendState(false);
                    break;
                case "hibernate":
                    baloon_text = "Гибернация";
                    SetSuspendState(true);
                    break;
                case "shutdown":
                    baloon_text = "Компьютер будет выключен через " + delay + " секунд";
                    args = " -s -t " + delay + " -c \"Компьютер будет выключен через " + delay + " секунд\"";
                    break;
                default:
                    baloon_text = "Команда не распознана! Указана задержка: " + delay + " секунд";
                    args = default;
                    break;
            }
            if (baloon_text != default) ShutdownApp.ShowBallon(baloon_text, ToolTipIcon.Info);
            if (args != default)
            {
                startInfo = new ProcessStartInfo("netsh")
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



        public static List<string> GetLocalIPv4()
        {
            List<string> output = new List<string>();
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(ip.Address))
                        {
                            output.Add(ip.Address.ToString());
                        }
                    }
                }
            }
            if (output.Count != 0) { return output; }
            else
            {
                output.Add("Не удалось определить IP");
                return output;
            }
        }



    }
}

