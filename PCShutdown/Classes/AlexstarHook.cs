using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Windows.Devices.Display.Core;
using System.Diagnostics;

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
            if (value != "{value}") 
            {
                actionType = (ShutdownTask.TaskType)Enum.Parse(typeof(ShutdownTask.TaskType), ShutdownApp.Cfg.AlexStar.TVInputs[value]);
            }
            else
            {
                actionType = ShutdownTask.TaskType.None;
            }
            return actionType;
        }

        public static void SetCurrentChannel(string channel)
        {
            
           /* switch (channel)
            {
                case 0:
                    //
                    var lst = Process.GetProcessesByName("WorldOfTanks");
                    if (lst.Length == 0)
                    {
                        Process.Start("\"C:\\Games\\Tanki\\WorldOfTanks.exe\"");
                    }
                    else
                    {
                        Process t = lst.First();
                        

                    }
                    break;
                default: MessageBox.Show(channel); break;
            }*/
        }
        public static int GetCurrentChannel()
        {
            var lst = Process.GetProcessesByName("WorldOfTanks");
            if (lst.Length != 0)
            {
                return 0;
            }
            else { return 1; }
        }
    }
}
