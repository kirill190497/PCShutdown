using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCShutdown.Classes
{
    internal class UrlProtocol
    {
        public static void Register()
        {
            try
            {
                var key = Registry.ClassesRoot.CreateSubKey("pcshutdown");
                key.SetValue("URL Protocol", "");
                var icon = key.CreateSubKey("DefaultIcon");
                icon.SetValue(null ,Application.ExecutablePath);
                var shell = key.CreateSubKey("Shell");
                var open = shell.CreateSubKey("Open");
                var command = open.CreateSubKey("Command");
                command.SetValue(null, Application.ExecutablePath + " %1");
            }
            catch (UnauthorizedAccessException)
            {
                var key = Registry.ClassesRoot.OpenSubKey("pcshutdown");
                var val = key.OpenSubKey("Shell").OpenSubKey("Open").OpenSubKey("Command").GetValue(null).ToString();
                if (key.GetValue("URL Protocol").ToString() != "" || val != Application.ExecutablePath + " %1")
                {
                    MessageBox.Show("Administrator rights required for first run");
                    Environment.Exit(0);
                }
                
            }
        }

        public static void Unregister()
        {

        }

        public static void ParseUrl(string url) 
        {
            Regex r = new(@"^(?<proto>\w+)://(?<cmd>\w+)?/(?<action>\w+)",
                          RegexOptions.None, TimeSpan.FromMilliseconds(150));
            Match m = r.Match(url);
            if (m.Success)
            {
                var proto = m.Groups["proto"].Value;
                if (proto == "pcshutdown")
                {
                    var action = m.Groups["action"].Value;
                    Request.GET(@"http://127.0.0.1:"+Server.localPort, $"password={Properties.Settings.Default.Password}&action={action}");
                   
                    Environment.Exit(0);
                }
                else
                {
                    MessageBox.Show($"Unable to parse url: {url}");
                    Environment.Exit(1);
                }
            }
        }
    }
}
