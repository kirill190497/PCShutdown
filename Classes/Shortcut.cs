using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCShutdown.Classes
{
    internal class Shortcut
    {
        public enum Action
        {
            None, Shutdown, Reboot, Sleep, Hibernate, Lock
        }
        public static void Create(string shortcut_name, Action shortcut_action) {
            string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            using (StreamWriter writer = new StreamWriter(deskDir + "\\" + shortcut_name + ".url"))
            {
                var action = Enum.GetName(typeof(Action), shortcut_action).ToLower();
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine($"URL=pcshutdown://runcommand/{action}");
                writer.WriteLine("IconIndex=" + (int)shortcut_action);
                string app = Application.ExecutablePath;
                string icon = app.Replace('\\', '/');
                writer.WriteLine("IconFile=" + icon);
            }
        }
    }
}
