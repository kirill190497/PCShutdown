using PCShutdown.Classes;
using System.Diagnostics;
using System.Windows.Forms;
using PCShutdown.Properties;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.IO;
using PCShutdown.Classes.DarkMode;
using System.Linq;

namespace PCShutdown
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 

        public static Config Cfg = Config.LoadConfig();


        static bool CheckFirewallRules()
        {

            using (Process process = new())
            {
                process.StartInfo.FileName = "netsh";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.Verb = "runas";
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.Arguments = "http show urlacl url=http://+:" + Server.localPort.ToString() + "/";
                process.Start();

                // Synchronously read the standard output of the spawned process.
                StreamReader reader = process.StandardOutput;
                string http_output = reader.ReadToEnd();

                if (http_output.Contains("http://+:" + Server.localPort.ToString() + "/"))
                {
                    process.StartInfo.Arguments = "advfirewall firewall show rule name=\"PCShutdown\"";
                    process.Start();

                    reader = process.StandardOutput;
                    string firewall_output = reader.ReadToEnd();

                    if (firewall_output.Contains("PCShutdown"))
                    {
                        Cfg.UrlAcl = true;
                        Cfg.Save();
                    }
                    else
                    {
                        Cfg.UrlAcl = false;
                        Cfg.Save();
                    }

                }
                else
                {
                    Cfg.UrlAcl = false;
                    Cfg.Save();
                }
                // Write the redirected output to this application's window.


                process.WaitForExit();
                return Cfg.UrlAcl;
            }
        }

        static void CheckUpdates()
        {
            var url = @"https://api.github.com/repos/kirill190497/PCShutdown/releases/latest";
            var remote = Request.GetJSON(url);
            var remoteVersion = new Version(remote["tag_name"].ToString());


            var localVersion = new Version(Application.ProductVersion.Split("+")[0]);
           
           

            // {major}.{minor}.{patch}
            // 1.1.0 - 1.0.2

            


            if (localVersion.CompareTo(remoteVersion) < 0)
            {
                DialogResult res;
                if (Cfg.DarkMode)
                {
                    res = Messenger.MessageBox("New update available! Download now?\nChangelog:\n" + remote["body"], "PCShutdown: Update available", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                else
                {
                    res = MessageBox.Show("New update available! Download now?\nChangelog:\n" + remote["body"], "PCShutdown: Update available", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                 
                if (res == DialogResult.Yes)
                {
                    var file_url = remote["assets"][0]["browser_download_url"].ToString();
                    var file_dest = Path.Combine(Cfg.WorkPath, file_url.Split("/")[^1]);
                    Request.SaveFile(file_url, file_dest);
                    while (true)
                    {
                        if (File.Exists(file_dest)) 
                        {
                            
                            break;
                        }
                    }
                    
                    Process.Start(Path.Combine(Cfg.WorkPath, "Updater.exe"), Cfg.WorkPath);
                }
            }

        }

        public static void Restart()
        {
            Process.Start(Application.ExecutablePath);
            Environment.Exit(0);
        }

        [STAThread]
        static void Main(string[] args)
        {
                       
            CheckUpdates();
#if !DEBUG
            UrlProtocol.Register();
            if (args.Length != 0)
            {
                UrlProtocol.ParseUrl(args[0]);
            }
#endif
            Application.ThreadException +=
                new ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(true);

            if (!CheckFirewallRules())
            {
                MessageBox.Show(ShutdownApp.Translation.Lang.Strings.AddingFirewallRules);
                string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                string add_http = "http add urlacl url=\"http://+:{0}/\" user={1}";
                string add_firewall = "advfirewall firewall add rule name=PCShutdown dir={0} protocol=tcp localport={1} action=allow";


                ProcessStartInfo startInfo = default;
                startInfo = new("netsh")
                {
                    UseShellExecute = false,
                    Verb = "runas",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "netsh",
                    Arguments = string.Format(add_http, Server.localPort, "Все"),
                    CreateNoWindow = true
                };
                Process.Start(startInfo);

                startInfo = new("netsh")
                {
                    UseShellExecute = false,
                    
                    Verb = "runas",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "netsh",
                    Arguments = string.Format(add_firewall, "in", Server.localPort),
                    CreateNoWindow = true
                };
                Process.Start(startInfo);

                startInfo = new("netsh")
                {
                    UseShellExecute = false,
                    Verb = "runas",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "netsh",
                    Arguments = string.Format(add_firewall, "out", Server.localPort),
                    CreateNoWindow = true
                };
                Process.Start(startInfo);

                if (CheckFirewallRules())
                {

                    Cfg.UrlAcl = true;

                    Cfg.Save();
                    MessageBox.Show(ShutdownApp.Translation.Lang.Strings.FirewallRulesAdded);
                }
                else
                {
                    MessageBox.Show(ShutdownApp.Translation.Lang.Strings.ErrorAddingFirewallRules);
                }

            }

            if (Cfg.UrlAcl)
            {


                ShutdownApp app = new();
                app.Start();
               
                Application.Run();
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception exception = e.Exception;
            

            //string body = message + "\n" + stacktrace + "\n" + source;

            var json = JsonConvert.SerializeObject(exception, Formatting.Indented);
            Messenger.MessageBox(json);

            //File.AppendAllText(Path.Combine(Cfg.WorkPath, @"crash.log"), json + Environment.NewLine);



        }

    }

}
