using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PCShutdown.Classes
{
    public class Config
    {
        public int Delay {  get; set; }
        public bool Autorun { get; set; }
        public string Password { get; set; }
        public bool UrlAcl { get; set; }
        public bool PasswordCheck { get; set; }
        public string WorkPath { get; set; }
        public int UnlockPin {  get; set; }
        public  bool CheckMAC {  get; set; }
        public string Language { get; set; }
        public string TelegramBotToken { get; set; }
        public int TelegramAdmin {  get; set; }
        public int ServerPort {  get; set; }
        public Dictionary<string, string> AlexStarTVInputs {  get; set; }
        public List<List<string>> TelegramMenu { get; set; }
        public  bool AdvancedSettings { get; set; }


        public static bool CheckConfig()
        {
            

            return true;
        }

        public static Config LoadConfig()
        {
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "config.json")))
            {
                string json = JsonConvert.SerializeObject(Properties.Settings.Default, Formatting.Indented);
                JObject obj = JObject.Parse(json);
                obj.Remove("Context");
                obj.Remove("Properties");
                obj.Remove("PropertyValues");
                obj.Remove("Providers");
                obj.Remove("SettingsKey");
                obj.Remove("IsSynchronized");
                obj["TelegramMenu"] = JToken.FromObject(new List<List<string>> {
                   new(){
                        "Pause",
                        "Mute",
                        "Screenshot"
                   },
                   new() {
                       "Lock",
                       "Unlock"
                   },
                   new()
                   {
                       "Shutdown",
                       "Sleep",
                       "Reboot"
                   }
               
                });
                var asi = obj["AlexStarTVInputs"].ToString().Split(";");
                var d = new Dictionary<string, string>();
                foreach (var item in asi)
                {
                    if (item != "")
                    {
                        var sp = item.Split('@');
                        d[sp[0]] = sp[1];
                    }

                }

                obj["AlexStarTVInputs"] = JToken.FromObject(d);

                File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "config.json"), obj.ToString());
            }

            var file = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "config.json"));

            Config config = JsonConvert.DeserializeObject<Config>(file);
            return config;
        }


        public void Save()
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);

            File.WriteAllText(Path.Combine(WorkPath, "config.json"), json);
        }

    }

    
}
