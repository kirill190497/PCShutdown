using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PCShutdown.Classes;
using GlobExpressions;
using System.Text.Json.Nodes;
using System.Collections.Generic;

using System.Linq;
using PCShutdown.Classes.DarkMode;
using System.Runtime.CompilerServices;


namespace PCShutdown.Forms
{
    public partial class ConfigForm : Form
    {


        public List<ComboBoxItem> LoadedLanguages = new List<ComboBoxItem>();
        private readonly Strings S = ShutdownApp.Translation.Lang.Strings;
        private readonly Config Cfg = ShutdownApp.Cfg;
        private bool AdvancedSettings { get; set; }
        private void InitForm()
        {
            kuzya_info.MaximumSize = new Size(panel2.Width - 15, 0);
            this.Icon = Icon.FromHandle(Resource.settings.GetHicon());
            WorkDirPath.Text = Cfg.WorkPath;
            delayValue.Text = Cfg.Delay.ToString();
            darkTheme.Checked = Cfg.DarkMode;
            autorun.Checked = Cfg.Autorun;
            passwordCheck.Checked = Cfg.PasswordCheck;
            password.Text = Cfg.Password;
            checkMac.Checked = Cfg.CheckMAC;
            unlock_pin.Text = Cfg.UnlockPin;
            TelegramBotToken.Text = Cfg.Telegram.BotToken;
            TelegramAdmin.Text = Cfg.Telegram.Admin.ToString();
            ServerPort.Text = Cfg.ServerPort.ToString();
            LoadLanguages();
            Language.SelectedItem = LoadedLanguages.Find(x => x.Value.Equals(Cfg.Language));
            if (WorkDirPath.Text == "")
            {
                WorkDirPath.Text = Directory.GetCurrentDirectory();
            }
            ApplyTranslation();
            if (Cfg.AdvancedSettings)
                AdvancedSettings = Cfg.AdvancedSettings;
            advancedGroup.Enabled = AdvancedSettings;

        }

        public ConfigForm()
        {

            InitializeComponent();

            InitForm();
        }
        public ConfigForm(bool AdvancedSettings)
        {
            InitializeComponent();
            this.AdvancedSettings = AdvancedSettings;

            InitForm();

        }
        private void ApplyTranslation()
        {
            this.Text = $"PCShutdown {Application.ProductVersion.Split("+")[0]} - " + S.Settings;
            if (AdvancedSettings)
            {
                Text = "PCShutdown - " + S.AdvancedSettings;
            }
            advancedGroup.Text = S.AdvancedSettings;
            LangLabel.Text = S.LanguageLabel;
            DelayLabel.Text = S.DelayLabel;
            delayValue.PlaceholderText = S.DelayLabel;
            PasswordLabel.Text = S.PasswordLabel;
            password.PlaceholderText = S.PasswordLabel;
            button1.Text = S.SaveButton;
            browse.Text = S.BrowseButton;
            autorun.Text = S.AutorunLabel;
            showPassword.Text = S.ShowPassword;
            showPin.Text = S.ShowPin;
            checkMac.Text = S.CheckMAC;
            passwordCheck.Text = S.CheckPassword;
            pin_label.Text = S.PinLabel;
            EditTelegramMenu.Text = S.EditTelegramMenu;
            darkTheme.Text = S.DarkTheme;
            YandexAliceGroup.Text = S.YandexAlice;

            kuzya_settings.Text = S.KuzyaSettingsButton;
            kuzya_info.Text = S.KuzyaSkillInfo;

        }
        private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void LoadLanguages()
        {
            string workpath = Cfg.WorkPath;
            var files = Glob.Files(Path.Combine(workpath, @"Lang/"), "*.json");


            foreach (var file in files)
            {

                try
                {
                    var filepath = Path.Combine(workpath, @"Lang", file);
                    var json = JsonNode.Parse(File.ReadAllText(filepath));
                    LoadedLanguages.Add(new ComboBoxItem(json["name"].ToString() + " - " + file, json["short"].ToString(), json["strings"]["RestartApp"].ToString(), json["strings"]["RestartQuestion"].ToString()));
                    Language.Items.Add(LoadedLanguages.Last());
                }
                catch (Exception)
                {
                    throw new ApplicationException(message: "Failed to load translation file \"" + file + "\"");
                }
            }

        }

        private void delayValue_KeyPress(object sender, KeyPressEventArgs e)
        {

            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool need_restart = false;

            Cfg.Delay = Convert.ToInt32(delayValue.Text);
            Cfg.Autorun = autorun.Checked;
            Cfg.PasswordCheck = passwordCheck.Checked;
            Cfg.Password = password.Text;
            Cfg.CheckMAC = checkMac.Checked;
            Cfg.UnlockPin = unlock_pin.Text;
            Cfg.ServerPort = Convert.ToInt32(ServerPort.Text);
            if (Cfg.DarkMode != darkTheme.Checked)
            {
                need_restart = true;
            }
            Cfg.DarkMode = darkTheme.Checked;
            if (Cfg.Telegram.Admin != Convert.ToInt32(TelegramAdmin.Text) || Cfg.Telegram.BotToken != TelegramBotToken.Text)
            {
                need_restart = true;
            }
            Cfg.WorkPath = WorkDirPath.Text;
            Cfg.Telegram.Admin = Convert.ToInt32(TelegramAdmin.Text);
            Cfg.Telegram.BotToken = TelegramBotToken.Text;
#if !DEBUG
            
            RegistryKey Run_cukey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            RegistryKey Paths_cukey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths\PCShutdown.exe");
            if (autorun.Checked)
            {
                var exefile_path = Path.Combine(WorkDirPath.Text, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".exe");
                Run_cukey.SetValue("PCShutdown", exefile_path);
                Paths_cukey.SetValue("", exefile_path);
                Paths_cukey.SetValue("Path", WorkDirPath.Text);

            }
            else
            {
                if (Run_cukey.GetValue("PCShutdown") != null)
                    Run_cukey.DeleteValue("PCShutdown");
            }
#endif
            var lang = ((ComboBoxItem)Language.SelectedItem);
            if (ShutdownApp.Translation.Lang.Short != lang.Value)
            {
                need_restart = true;
                Cfg.Language = lang.Value;
            }
            if (need_restart)
            {
                var answer = MessageBox.Show(lang.Question, lang.ShortQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    need_restart = true;
                }
                else
                {
                    need_restart = false;
                }

            }
            Cfg.Save();
            Hide();
            string text = string.Format("{0}: {1}\n{2}: {3}\n{4}\n{5}: {6}\n{7}: {8}\n{9}: {10}",
                S.AutorunLabel, autorun.Checked ? S.On : S.Off, S.CheckPassword, // {0} {1}
                passwordCheck.Checked ? S.Yes : S.No, // {2} {3}
                passwordCheck.Checked ? S.PasswordLabel + ": " + password.Text : "", // {4}
                S.LanguageLabel, lang.Text, // {5} {6}
                S.WorkingPath, WorkDirPath.Text, // {7} {8}
                S.DarkTheme, darkTheme.Checked ? S.Yes : S.No);
            ShutdownApp.ShowToast(text, S.SettingsSaved);

            if (need_restart)
            {
                Program.Restart();
            }

        }

        private void showPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (showPassword.Checked)
                password.UseSystemPasswordChar = false;
            else
                password.UseSystemPasswordChar = true;
        }



        private void browse_Click(object sender, EventArgs e)
        {
            DialogResult result = workPathDialog.ShowDialog();

            if (result.Equals(DialogResult.OK))
            {
                WorkDirPath.Text = workPathDialog.SelectedPath;
            }
        }

        private void WorkDirPath_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = workPathDialog.ShowDialog();

            if (result.Equals(DialogResult.OK))
            {
                WorkDirPath.Text = workPathDialog.SelectedPath;
            }
        }

        private void showPin_CheckedChanged(object sender, EventArgs e)
        {
            if (showPin.Checked)
                unlock_pin.UseSystemPasswordChar = false;
            else
                unlock_pin.UseSystemPasswordChar = true;
        }

        private void delayValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void advancedGroup_Enter(object sender, EventArgs e)
        {

        }

        private void TelegramAdmin_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8 && number != 22) // цифры и клавиша BackSpace + CTRL_V
            {
                e.Handled = true;
            }

        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {

        }

        private void EditTelegramMenu_Click(object sender, EventArgs e)
        {
            ShutdownApp.Forms.ShowForm(typeof(TelegramMenuForm));
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kuzya_settings_Click(object sender, EventArgs e)
        {
            ShutdownApp.Forms.ShowForm(typeof(KuzyaSettingsForm));
        }
    }
}
