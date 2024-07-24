﻿using Microsoft.Toolkit.Uwp.Notifications;

using PCShutdown.Forms;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using PCShutdown.Classes.TelegramBot;
using System.Collections.Generic;

namespace PCShutdown.Classes
{

    internal class ShutdownApp
    {
        public static Config Cfg = Program.Cfg;
        static NotifyIcon tray = new();
        ContextMenuStrip menu = new();
        public static Translation Translation = new(Cfg.Language);
        public static FormsManager Forms = new();
        public static Form GlobalForm;
        
        
        int delay = Cfg.Delay;
        public void Start()
        {
            GlobalForm = new()
            {
                WindowState = FormWindowState.Minimized
            };
            GlobalForm.Show();
            GlobalForm.Hide();
            ToastNotificationManagerCompat.History.Clear();
            if (Cfg.TelegramBotToken != "" & Cfg.TelegramAdmin != 0) 
            {
                 ShutdownTelegramBot.Run();
            }

            

            ToolStripItem interfaces = menu.Items.Add(Translation.Lang.Strings.NetworkInterfaces, Resource.ipaddress2);

            foreach (var item in Server.GetNetworkInterfaces())
            {
                ToolStripItem netif = (interfaces as ToolStripMenuItem).DropDownItems.Add(item.Name + "\n" + item.Description, Resource.networkcard);
                (netif as ToolStripMenuItem).DropDownItems.Add(item.IPv4.ToString(), Resource.ipaddress, OpenIP_Link);
                (netif as ToolStripMenuItem).DropDownItems.Add(item.HardwareAddress.ToString(), Resource.processor, CopyHWID);
                (netif as ToolStripMenuItem).DropDownItems.Add(item.Description, Resource.comment);
            }

            
            PowerStatus pwr = SystemInformation.PowerStatus;
            
            string strBatteryChargingStatus = (pwr.BatteryLifePercent * 100) +"% "+ pwr.BatteryChargeStatus.ToString() ;
            menu.Items.Add(strBatteryChargingStatus, Resource.battery);
            
            menu.Items.Add(new ToolStripSeparator());

            menu.Items.Add(Translation.Lang.Strings.ShutdownPC, Resource.power, OnShutdown);
            menu.Items.Add(Translation.Lang.Strings.RebootPC, Resource.reboot, OnReboot);
            menu.Items.Add(Translation.Lang.Strings.LockScreen, Resource.padlock, OnLock);
            menu.Items.Add(Translation.Lang.Strings.Hibernation, Resource.hibernate, OnHibernate);
            menu.Items.Add(Translation.Lang.Strings.Sleep, Resource.sleep, OnSleep);




            menu.Items.Add(new ToolStripSeparator());
            ToolStripItem tasks = menu.Items.Add(Translation.Lang.Strings.Tasks, Resource.tasks, OnTasks);
            ToolStripItem links = menu.Items.Add(Translation.Lang.Strings.Shortcuts, Resource.shortcuts);
            (links as ToolStripMenuItem).DropDownItems.Add(Translation.Lang.Strings.Sleep, Resource.sleep, CreateShortcut);
            (links as ToolStripMenuItem).DropDownItems.Add(Translation.Lang.Strings.ShutdownPC, Resource.power, CreateShortcut);
            (links as ToolStripMenuItem).DropDownItems.Add(Translation.Lang.Strings.Hibernation, Resource.hibernate, CreateShortcut);
            (links as ToolStripMenuItem).DropDownItems.Add(Translation.Lang.Strings.LockScreen, Resource.padlock, CreateShortcut);
            (links as ToolStripMenuItem).DropDownItems.Add(Translation.Lang.Strings.RebootPC, Resource.reboot, CreateShortcut);
            
            menu.Items.Add(Translation.Lang.Strings.Settings, Resource.settings, OnSettings);
            menu.Items.Add(Translation.Lang.Strings.CancelTasks, Resource.save, OnCancel);
            menu.Items.Add(Translation.Lang.Strings.ExitApp, Resource.close, OnExit);

            


            tray.Icon = new Icon(Resource.icon, 40, 40);
#if DEBUG
            tray.Icon = new Icon(Resource.icon_debug, 40, 40);

#endif
            tray.Text = "PCShutdown";
            tray.DoubleClick += Tray_Click;

            tray.Visible = true;
            tray.ContextMenuStrip = menu;

            Server.Start();
        }

        public static async void ShowToast(string text, string attrib_text = "")
        {

            await Task.Run(() => {
                var toast = new ToastContentBuilder();
                toast.AddArgument("action", "viewConversation");
                toast.AddArgument("conversationId", 1);
                toast.AddText(text);
                toast.AddAttributionText(attrib_text);
                toast.SetToastDuration(ToastDuration.Short);
                
                toast.Show();
            });

        }

        private void CopyHWID(object sender, EventArgs e)
        {
            Clipboard.SetText(sender.ToString());
            ShowToast(string.Format("MAC {0}", sender), Translation.Lang.Strings.CopiedToClipboard);
        }

        public static void UpdateTasksListForm()
        {
            Forms.ShowForm(typeof(TasksForm));
        }

        private void OpenIP_Link(object sender, EventArgs e)
        {
            var net_if = Server.GetInterfaceByIP(sender.ToString());

            string url = "http://" + net_if.IPv4 + ":" + Server.localPort + "/?password=" + ShutdownApp.Cfg.Password + "&hwid=" + net_if.HardwareAddress;

            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        private void OnExit(object sender, EventArgs e)
        {
            Server.Stop();
            Application.Exit();
        }

        
        private void CreateShortcut(object sender, EventArgs e)
        {
            Shortcut.Action action;
            var name = sender.ToString();
            if (name == Translation.Lang.Strings.RebootPC)
            {
                action = Shortcut.Action.Reboot;
            }
            else if (name == Translation.Lang.Strings.ShutdownPC)
            {
                action = Shortcut.Action.Shutdown;
            }
            else if (name == Translation.Lang.Strings.Sleep)
            {
                action = Shortcut.Action.Sleep;
            }
            else if (name == Translation.Lang.Strings.Hibernation)
            {
                action = Shortcut.Action.Hibernate;
            }
            else if (name == Translation.Lang.Strings.LockScreen)
            {
                action = Shortcut.Action.Lock;
            }
            else
                action = Shortcut.Action.None;
            Shortcut.Create(name, action);
        }
        private void OnTasks(object sender, EventArgs e)
        {
            Forms.ShowForm(typeof(TasksForm));
        }



        private void OnLock(object sender, EventArgs e)
        {
            Server.ExecCommand(ShutdownTask.TaskType.Lock);      
        }

        private void OnSettings(object sender, EventArgs e)
        {
            if (Control.ModifierKeys.Equals(Keys.Shift))
                Forms.ShowForm(typeof(ConfigForm), true );
            else
                Forms.ShowForm(typeof(ConfigForm));
        }

        private void OnShutdown(object sender, EventArgs e)
        {
            Server.ExecCommand(ShutdownTask.TaskType.ShutdownPC, delay);

        }
        private void OnReboot(object sender, EventArgs e)
        {

            Server.ExecCommand(ShutdownTask.TaskType.RebootPC, delay);

        }

        private void OnHibernate(object sender, EventArgs e)
        {
            Server.ExecCommand(ShutdownTask.TaskType.Hibernaiton);
        }

        private void OnSleep(object sender, EventArgs e)
        {
            Server.ExecCommand(ShutdownTask.TaskType.Sleep);
        }

        private void OnCancel(object sender, EventArgs e)
        {
            Server.ExecCommand(ShutdownTask.TaskType.Cancel, delay);
        }

        private void Tray_Click(object sender, EventArgs e)
        {
           Forms.ShowForm(typeof(ConfigForm));
        }

        
    }
}