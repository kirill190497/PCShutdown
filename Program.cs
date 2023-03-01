

using PCShutdown.Classes;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PCShutdown.Properties;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace PCShutdown
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 




        static bool CheckFirewallRules()
        {

            using (Process process = new())
            {
                process.StartInfo.FileName = "netsh";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.Verb = "runas";
                process.StartInfo.CreateNoWindow= true;
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
                        Settings.Default.UrlAcl = true;
                        Settings.Default.Save();
                    }
                    else
                    {
                        Settings.Default.UrlAcl = false;
                        Settings.Default.Save();
                    }

                }
                else
                {
                    Settings.Default.UrlAcl = false;
                    Settings.Default.Save();
                }
                // Write the redirected output to this application's window.


                process.WaitForExit();
                return Settings.Default.UrlAcl;
            }
        }

        [STAThread]
        static void Main(string[] args)
        {
            UrlProtocol.Register();
            if (args.Length != 0)
            {
                UrlProtocol.ParseUrl(args[0]);
            }
            Application.ThreadException +=
                new ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

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
                    Arguments = string.Format(add_http, Server.localPort, "���"),
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

                    Settings.Default.UrlAcl = true;

                    Settings.Default.Save();
                    MessageBox.Show(ShutdownApp.Translation.Lang.Strings.FirewallRulesAdded);
                }
                else
                {
                    MessageBox.Show(ShutdownApp.Translation.Lang.Strings.ErrorAddingFirewallRules);
                }

            }

            if (Settings.Default.UrlAcl)
            {


                ShutdownApp app = new();
                app.Start();

                Application.Run();
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception exception = e.Exception;
            string message = exception.Message;
            string stacktrace = exception.StackTrace;
            string source = exception.Source;
            if (exception.InnerException != null)
            {
                message = exception.InnerException.Message;
                stacktrace = exception.InnerException.StackTrace;
                source = exception.InnerException.Source;
            }

            //string body = message + "\n" + stacktrace + "\n" + source;

            MessageBox.Show(message, "Application error!");



        }

    }

}
