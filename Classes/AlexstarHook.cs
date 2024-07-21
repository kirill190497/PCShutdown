using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Windows.Devices.Display.Core;


namespace PCShutdown.Classes
{
    public static class AlexstarHook
    {
        public static string GetCurentInput()
        {
            string input;
            

   
            input = true ? "one" : "two";

            return input;
        }

        public static ShutdownTask.TaskType GetCommandByInput(string value) 
        {
            ShutdownTask.TaskType actionType;
            Dictionary<string, ShutdownTask.TaskType> dict = new();

            foreach (var i in Properties.Settings.Default.AlexStarTVInputs.Split(";"))
            {
                if (i != null && i != "")
                {
                    
                    var s = i.Split('@');
                    string key = s[0];
                    
                    ShutdownTask.TaskType valueType = (ShutdownTask.TaskType)Enum.Parse(typeof(ShutdownTask.TaskType), s[1]);

                    dict.Add(key, valueType);
                }
                
            }


            if (value != "{value}") 
            {
                actionType = dict[value];
            }
            else
            {
                actionType = ShutdownTask.TaskType.None;
            }
            return actionType;
        }
    }
}
