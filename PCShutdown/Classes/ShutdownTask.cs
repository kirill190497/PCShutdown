using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace PCShutdown.Classes
{
    public class ShutdownTask
    {
        public enum TaskType { ShutdownPC, RebootPC, Lock, Unlock, Sleep, Hibernaiton, Notification, Cancel, ScreenOff, ScreenOn, MediaPause, MediaNext, MediaPrev, VolumeMute, VolumeUp, VolumeDown, Screenshot, None }
        public DateTime Date { get; set; }
        public int ID { get; set; }
        public TaskType Type { get; set; }
        public string Comment { get; set; }
        public string ToastImage { get; set; }
        public ShutdownTask(int id, TaskType type, DateTime date, string comment)
        {
            ID = id;
            Type = type;
            Date = date;
            Comment = comment;
        }

        public string GetTranslatedTypeName()
        {
            
            Type t = ShutdownApp.Translation.Lang.Strings.GetType();
            PropertyInfo[] props = t.GetProperties();
            
                // prop.Name  prop.GetValue(ShutdownApp.Translation.Lang.Strings).ToString());//  
            
           
                foreach (PropertyInfo prop in props)
                {
                    if (prop.Name == Type.ToString())
                    {
                        return prop.GetValue(ShutdownApp.Translation.Lang.Strings).ToString();
                    }
                 
                }
            return Type.ToString();
        }

        public static string GetTranslatedTypeName(TaskType type)
        {

            Type t = ShutdownApp.Translation.Lang.Strings.GetType();
            PropertyInfo[] props = t.GetProperties();

            // prop.Name  prop.GetValue(ShutdownApp.Translation.Lang.Strings).ToString());//  


            foreach (PropertyInfo prop in props)
            {
                if (prop.Name == type.ToString())
                {
                    return prop.GetValue(ShutdownApp.Translation.Lang.Strings).ToString();
                }

            }
            return type.ToString();
        }
        public static string GetTranslatedTypeName(string type)
        {

            Type t = ShutdownApp.Translation.Lang.Strings.GetType();
            PropertyInfo[] props = t.GetProperties();

            // prop.Name  prop.GetValue(ShutdownApp.Translation.Lang.Strings).ToString());//  


            foreach (PropertyInfo prop in props)
            {
                
                if (prop.Name == type)
                {
                    return prop.GetValue(ShutdownApp.Translation.Lang.Strings).ToString();
                }

            }
            return type;
        }
    }
}
