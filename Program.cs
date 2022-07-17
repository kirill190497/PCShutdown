
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

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

            using (Process process = new Process())
            {
                process.StartInfo.FileName = "netsh";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.Verb = "runas";
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
                        Properties.Settings.Default.UrlAcl = true;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        Properties.Settings.Default.UrlAcl = false;
                        Properties.Settings.Default.Save();
                    }

                }
                else
                {
                    Properties.Settings.Default.UrlAcl = false;
                    Properties.Settings.Default.Save();
                }
                // Write the redirected output to this application's window.


                process.WaitForExit();
                return Properties.Settings.Default.UrlAcl;
            }
        }

        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!CheckFirewallRules())
            {
                MessageBox.Show("Добавляю исключения в фаервол. ");
                string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                string add_http = "http add urlacl url=\"http://+:{0}/\" user={1}";
                string add_firewall = "advfirewall firewall add rule name=PCShutdown dir={0} protocol=tcp localport={1} action=allow";


                ProcessStartInfo startInfo = default;
                startInfo = new ProcessStartInfo("netsh")
                {
                    UseShellExecute = true,
                    Verb = "runas",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "netsh",
                    Arguments = String.Format(add_http, Server.localPort, "Все"),
                    CreateNoWindow = true
                };
                Process.Start(startInfo);

                startInfo = new ProcessStartInfo("netsh")
                {
                    UseShellExecute = true,
                    Verb = "runas",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "netsh",
                    Arguments = String.Format(add_firewall, "in", Server.localPort),
                    CreateNoWindow = true
                };
                Process.Start(startInfo);

                startInfo = new ProcessStartInfo("netsh")
                {
                    UseShellExecute = true,
                    Verb = "runas",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "netsh",
                    Arguments = String.Format(add_firewall, "out", Server.localPort),
                    CreateNoWindow = true
                };
                Process.Start(startInfo);

                if (CheckFirewallRules())
                {
                    Properties.Settings.Default.UrlAcl = true;

                    Properties.Settings.Default.Save();
                    MessageBox.Show("Исключения добавлены, запуск.");
                }
                else
                {
                    MessageBox.Show("Ошибка добавления правил фаервола, попробуйте еще раз");
                }

            }







            if (Properties.Settings.Default.UrlAcl)
            {
                ShutdownApp app = new ShutdownApp();
                app.Start();

                Application.Run();
            }


            //Console.ReadKey();
        }





    }

    class ShutdownApp
    {
        static NotifyIcon tray = new NotifyIcon();
        ContextMenuStrip menu = new ContextMenuStrip();

        ConfigForm configForm = new ConfigForm();
        ToolStripItem ip;

        int delay = Properties.Settings.Default.Delay;
        public void Start()
        {
            ip = menu.Items.Add(Server.GetLocalIPv4()[0].ToString(), Resource.settings, UpdateIP);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add("Выключить ПК", Resource.power, OnShutdown);
            menu.Items.Add("Перезагрузить ПК", Resource.reboot, OnReboot);
            menu.Items.Add("Заблокировать", Resource.padlock, OnLock);
            menu.Items.Add("Гибернация", Resource.hibernate, OnHibernate);
            menu.Items.Add("Сон", Resource.sleep, OnSleep);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add("Настройки", Resource.settings, OnSettings);
            menu.Items.Add("Отменить отключение", Resource.save, OnCancel);
            menu.Items.Add("Выход", Resource.close, OnExit);


            tray.Icon = new Icon(Resource.icon, 40, 40);
            tray.Text = "PCShutdown";
            tray.DoubleClick += tray_Click;

            tray.Visible = true;
            tray.ContextMenuStrip = menu;

            Server.Start();
            //await Task.Run(StartListener);
        }

        public static void ShowBallon(string text, ToolTipIcon icon = ToolTipIcon.Info, int delay = 3000)
        {
            tray.BalloonTipIcon = icon;
            tray.BalloonTipText = text;
            if (text.Length > 0)
            {
                tray.ShowBalloonTip(delay);
            }

        }


        private void UpdateIP(object sender, EventArgs e)
        {
            ip.Text = Server.GetLocalIPv4()[0].ToString();
            menu.Show();
        }
        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }


        [DllImport("user32.dll")]
        static extern bool LockWorkStation();
        [DllImport("PowrProf.dll")]
        static extern bool SetSuspendState(bool bHibernate);


        private void OnLock(object sender, EventArgs e)
        {
            Server.execCommand("lock");
        }

        private void OnSettings(object sender, EventArgs e)
        {
            configForm.Show();
        }

        private void OnShutdown(object sender, EventArgs e)
        {
            Server.execCommand("shutdown", delay);

        }

        private void OnReboot(object sender, EventArgs e)
        {

            Server.execCommand("reboot", delay);

        }

        private void OnHibernate(object sender, EventArgs e)
        {
            Server.execCommand("hibernate");
        }

        private void OnSleep(object sender, EventArgs e)
        {
            Server.execCommand("sleep");
        }

        private void OnCancel(object sender, EventArgs e)
        {
            ShowBallon("Отмена операции отключения/перезагрузки", ToolTipIcon.Info);
            Server.execCommand("cancel", delay);
        }




        private void tray_Click(object sender, EventArgs e)
        {
            configForm.Show();
        }

        private void tray_exitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
