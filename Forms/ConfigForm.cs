using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PCShutdown.Classes;
using GlobExpressions;
using System.Text.Json.Nodes;
using System.Collections.Generic;
using PCShutdown.Properties;
using System.Linq;

namespace PCShutdown.Forms
{
    public partial class ConfigForm : Form
    {

        public List<ComboBoxItem> LoadedLanguages = new List<ComboBoxItem>();
        private readonly Strings S = ShutdownApp.Translation.Lang.Strings;
        private bool AdvancedSettings { get; set; }
        private void InitForm()
        {
            this.Icon = Icon.FromHandle(Resource.settings.GetHicon());
            WorkDirPath.Text = Settings.Default.WorkPath;
            delayValue.Text = Settings.Default.Delay.ToString();

            autorun.Checked = Settings.Default.Autorun;
            passwordCheck.Checked = Settings.Default.PasswordCheck;
            password.Text = Settings.Default.Password;
            checkMac.Checked = Settings.Default.CheckMAC;
            unlock_pin.Text = Settings.Default.UnlockPin;
            TelegramBotToken.Text = Settings.Default.TelegramBotToken;
            TelegramAdmin.Text = Settings.Default.TelegramAdmin.ToString();
            ServerPort.Text = Settings.Default.ServerPort.ToString();
            LoadLanguages();
            Language.SelectedItem = LoadedLanguages.Find(x => x.Value.Equals(Settings.Default.Language));
            if (WorkDirPath.Text == "")
            {
                WorkDirPath.Text = Directory.GetCurrentDirectory();
            }
            ApplyTranslation();

            advancedGroup.Enabled = AdvancedSettings;

        }

        public ConfigForm()
        {
            InitializeComponent();
            this.AdvancedSettings = false;
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
            this.Text = "PCShutdown - " + S.Settings;
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
        }
        private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
                
            }*/
        }

        private void LoadLanguages()
        {
            string workpath = Settings.Default.WorkPath;
            var files = Glob.Files(Path.Combine(workpath, @"Lang/"), "*.json");


            foreach (var file in files)
            {

                try
                {
                    var filepath = Path.Combine(workpath, @"Lang", file);
                    var json = JsonNode.Parse(File.ReadAllText(filepath));
                    LoadedLanguages.Add(new ComboBoxItem(json["name"].ToString() + " - " + file, json["short"].ToString(), json["strings"]["RestartApp"].ToString()));
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
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool need_restart = false;
            Settings.Default.Delay = Convert.ToInt32(delayValue.Text);
            Settings.Default.Autorun = autorun.Checked;
            Settings.Default.PasswordCheck = passwordCheck.Checked;
            Settings.Default.Password = password.Text;
            Settings.Default.TelegramBotToken = TelegramBotToken.Text;
            Settings.Default.CheckMAC = checkMac.Checked;
            Settings.Default.UnlockPin = unlock_pin.Text;
            Settings.Default.ServerPort = Convert.ToInt32(ServerPort.Text);
            Settings.Default.TelegramAdmin = Convert.ToInt32(TelegramAdmin.Text);
#if !DEBUG
            Settings.Default.WorkPath = WorkDirPath.Text;
            RegistryKey cukey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            if (autorun.Checked)
            {

                cukey.SetValue("PCShutdown", WorkDirPath.Text + "\\" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".exe");
            }
            else
            {
                if (cukey.GetValue("PCShutdown") != null)
                    cukey.DeleteValue("PCShutdown");
            }
#endif
            var lang = ((ComboBoxItem)Language.SelectedItem);
            if (ShutdownApp.Translation.Lang.Short != lang.Value)
            {
                Settings.Default.Language = lang.Value;

                var answer = MessageBox.Show(lang.Question, "Change language?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    need_restart = true;
                }

            }
            Settings.Default.Save();
            this.Hide();
            string text = string.Format("{0}: {1}\n{2}: {3}\n{4}\n{5}: {6}\n{7}: {8}",
                S.AutorunLabel, autorun.Checked ? S.On : S.Off, S.CheckPassword, // {0} {1}
                passwordCheck.Checked ? S.Yes : S.No, // {2} {3}
                passwordCheck.Checked ? S.PasswordLabel + ": " + password.Text : "", // {4}
                S.LanguageLabel, lang.Text, // {5} {6}
                S.WorkingPath, WorkDirPath.Text); // {7} {8}
            ShutdownApp.ShowToast(text, S.SettingsSaved);

            if (need_restart)
            {
                System.Diagnostics.Process.Start(Application.ExecutablePath);
                Environment.Exit(0);
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
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
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
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
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
    }
}
