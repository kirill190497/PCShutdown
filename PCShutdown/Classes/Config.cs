using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace PCShutdown.Classes
{
    public class AlexStar
    {
        public Dictionary<string, string> TVInputs { get; set; }
        public Dictionary<string, Dictionary<string, string>> Channels { get; set; }
    }

    public class TelegramConfig
    {
        public string BotToken { get; set; }
        public int Admin { get; set; }
    
    }
    public class Config
    {
        public int Delay {  get; set; }
        public bool Autorun { get; set; }
        public string Password { get; set; }
        public bool UrlAcl { get; set; }
        public bool PasswordCheck { get; set; }
        public string WorkPath { get; set; }
        public string UnlockPin {  get; set; }
        public  bool CheckMAC {  get; set; }
        public string Language { get; set; }
        //public string TelegramBotToken { get; set; }
        //public int TelegramAdmin {  get; set; }
        public TelegramConfig Telegram {  get; set; }
        public int ServerPort {  get; set; }
        public AlexStar AlexStar { get; set; }
        public List<List<string>> TelegramMenu { get; set; }
        public  bool AdvancedSettings { get; set; }
        public bool DarkMode { get; set; }

        private static string ConfigPath = Application.ExecutablePath.Replace(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".exe", "config.json");

        public static Config CheckConfig()
        {
            JSchemaGenerator generator = new JSchemaGenerator();
            Config cfg = new();
            JSchema schema = generator.Generate(typeof(Config));
            IList<string> messages = new List<string>();
            try
            {
                var file = File.ReadAllText(ConfigPath);
                JsonTextReader reader = new JsonTextReader(new StringReader(file));

                JSchemaValidatingReader validatingReader = new JSchemaValidatingReader(reader);
                validatingReader.Schema = schema;
                
                validatingReader.ValidationEventHandler += (o, a) => messages.Add(a.Message);
                

                JsonSerializer serializer = new JsonSerializer();
                cfg = serializer.Deserialize<Config>(validatingReader);

            }
            catch { }
            finally {
                string message = "";
                foreach (var err in messages)
                {
                    message += err.ToString() + "\n";
                    if (err.Contains("object: AlexStar"))
                    {
                        var inp = new Dictionary<string, string>
                        {
                            ["one"] = ShutdownTask.TaskType.ScreenOff.ToString(),
                            ["two"] = ShutdownTask.TaskType.ScreenOn.ToString(),
                            ["three"] = ShutdownTask.TaskType.MediaPause.ToString(),
                            ["four"] = ShutdownTask.TaskType.MediaNext.ToString(),
                            ["five"] = ShutdownTask.TaskType.MediaPrev.ToString(),
                            ["six"] = ShutdownTask.TaskType.Cancel.ToString(),
                            ["seven"] = ShutdownTask.TaskType.Screenshot.ToString(),
                            ["eight"] = ShutdownTask.TaskType.Sleep.ToString(),
                            ["nine"] = ShutdownTask.TaskType.Lock.ToString(),
                            ["ten"] = ShutdownTask.TaskType.Unlock.ToString()
                        };
                        cfg.AlexStar = new AlexStar
                        {
                            TVInputs = inp
                        };
                        cfg.Save();
                        Program.Restart();
                    }
                    if (err.Contains("object: Telegram"))
                    {
                        var json = JObject.Parse(File.ReadAllText(ConfigPath));

                        var tg = new TelegramConfig
                        {
                            BotToken = "",
                            Admin = 0,
                        };
                        if (json["TelegramBotToken"].ToString() != "")
                        {
                            tg.BotToken = json["TelegramBotToken"].ToString();
                        }
                        if ((int)json["TelegramAdmin"] != 0)
                        {
                            tg.Admin = (int)json["TelegramAdmin"];
                        }
                        cfg.Telegram = tg;
                        cfg.Save();
                        Program.Restart();
                    }
                }
                if (message != "")
                {
                    MessageBox.Show(message, "Config error!");
                    Environment.Exit(1);
                }
            }
            return cfg;
        }

        public static Config LoadConfig()
        {

            if (!File.Exists(ConfigPath))
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
                       "LockScreen",
                       "UnlockScreen"
                   },
                   new()
                   {
                       "ShutdownPC",
                       "Sleep",
                       "RebootPC"
                   }
               
                });
                obj["DarkMode"] = true;
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
                obj["AlexStar"] = JToken.FromObject(new AlexStar { TVInputs = d});
                //obj["AlexStar"]["TVInputs"] = JToken.FromObject(d);

                File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "config.json"), obj.ToString());
            }
            return CheckConfig();
            
            
        }


        public void Save()
        {
            // var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            JSchemaGenerator generator = new JSchemaGenerator();
            
            JSchema schema = generator.Generate(typeof(Config));
            StringWriter stringWriter = new StringWriter();
            JsonTextWriter writer = new JsonTextWriter(stringWriter);
            writer.Formatting = Formatting.Indented;

            JSchemaValidatingWriter validatingWriter = new JSchemaValidatingWriter(writer);
            validatingWriter.Schema = schema;

            IList<string> messages = new List<string>();
            validatingWriter.ValidationEventHandler += (o, a) => messages.Add(a.Message);

            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(validatingWriter, this);


            File.WriteAllText(ConfigPath,stringWriter.ToString());
        }

    }

    
}
