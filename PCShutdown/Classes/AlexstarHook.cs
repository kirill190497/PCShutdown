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
           

            //ShutdownTask.TaskType valueType = (ShutdownTask.TaskType)Enum.Parse(typeof(ShutdownTask.TaskType), s[1]);
            if (value != "{value}") 
            {
                actionType = (ShutdownTask.TaskType)Enum.Parse(typeof(ShutdownTask.TaskType), ShutdownApp.Cfg.AlexStarTVInputs[value]);
            }
            else
            {
                actionType = ShutdownTask.TaskType.None;
            }
            return actionType;
        }
    }
}
