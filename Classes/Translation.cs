using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCShutdown.Classes
{
    class Strings
    {
        // config form strings
        public string SaveButton { get; set; }
        public string DelayLabel { get; set; }
        public string PasswordLabel { get; set; }
        public string ShowPassword { get; set; }
        public string PinLabel { get; set; }
        public string ShowPin { get; set; }
        public string AutorunLabel { get; set; }
        public string CheckPassword { get; set; }
        public string CheckMAC { get; set; }
        public string LanguageLabel { get; set; }
        public string BrowseButton { get; set; }
        public string Settings { get; set; }

        // 
        public string AddingFirewallRules { get; set; }
        public string FirewallRulesAdded { get; set; }
        public string ErrorAddingFirewallRules { get; set; }
        public string ApplicationError { get; set; }

        public string RestartApp { get; set; }
        public string NetworkInterfaces { get; set; }
        public string ShutdownPC { get; set; }
        public string RebootPC { get; set; }
        public string LockScreen { get; set; }
        public string Hibernation { get; set; }
        public string Sleep { get; set; }
        public string Tasks { get; set; }
        public string CancelTasks { get; set; }
        public string ExitApp { get; set; }
        public string CopiedToClipboard { get; set; }
        public string Yes { get; set; }
        public string No { get; set; }
        public string On { get; set; }
        public string Off { get; set; }
        public string SettingsSaved { get; set; }
        public string WorkingPath { get; set; }
        public string Action { get; set; }
        public string Message { get; set; }
        public string ExecTime { get; set; }
        public string SendButton { get; set; }
        public string Notification { get; set; }
        public string ExceptionOccurred { get; set; }
        public string ErrorOccurred { get; set; }
        public string LaunchError { get; set; }
        public string Back { get; set; }
        public string CommandDone { get; set; }
        public string ActionNotSet { get; set; }
        public string Home { get; set; }
        public string WrongMac { get; set; }
        public string WrongPassword { get; set; }
        public string EnterPassword { get; set; }
        public string QueueCleared { get; set; }
        public string TasksCanceled { get; set; }
        public string WillRebooted { get; set; }
        public string WillShutdowned { get; set; }
        public string WrongCommand { get; set; }
        public string NoActiveInterface { get; set; }
        public string TaskType { get; set; }
        public string TaskTime { get; set; }
        public string TaskMessage { get; set; }
        public string TaskNotSelected { get; set; }
        public string TaskAdded { get; set; }
        public string AddTaskGroup { get; set; }
        public string DeleteTaskGroup { get; set; }
        public string AddInTimeButton { get; set; }
        public string AddByTimerButton { get; set; }
        public string RemoveAllButton { get; set; }
        public string RemoveSelectedButton { get; set; }
        public string NoteGroup { get; set; }
        public string NoteText { get; set; }
        public string UpdateListButton { get; set; }
        public string SelectedTasksRemoved { get; set; }
        public string TurnOffScreen { get; set; }
        public string TurnOnScreen { get; set; }
        public string Minutes { get; set; }
        public string Hours { get; set; }
        public string AfterLabel { get; set; }
        public string SmallIntervalError { get; set; }
        public string SetDifferentTime { get; set; }
        public string AdvancedSettings { get; set;}
        public string Shortcuts { get; set; }
    }
    class Lang
    {
        public string Name;
        public string Short;

        public Strings Strings;
        
    }
    

    internal class Translation
    {   
        public Lang Lang;
        public Translation(string lang) 
        {
            var path = Path.Combine(Properties.Settings.Default.WorkPath, @"Lang", lang + ".json");

            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                Lang = (Lang)serializer.Deserialize(file, typeof(Lang));
            }

#if !DEBUG
            var errors = GetNullValues(Lang.Strings);
            if (errors.Count != 0)
            {
                var msg = "Failed to get string values: " + string.Join(",", errors); ;
                
                
                
                MessageBox.Show(msg);
            }
#endif
        }



        private static List<string> GetNullValues(object obj)
        {
            Type t = obj.GetType();
            //Console.WriteLine("Type is: {0}", t.Name);
            PropertyInfo[] props = t.GetProperties();
            //Console.WriteLine("Properties (N = {0}):", props.Length);
            List<string> err = new List<string>();
            foreach (var prop in props)
                //ShutdownApp.ShowToast(prop.Name);
                if (prop.GetValue(obj) == null)
                   err.Add(prop.Name);

            return err;
        }
    }


}
