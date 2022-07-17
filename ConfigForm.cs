using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PCShutdown
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
            this.Text = "PCShutdown - Настройки";
            this.Icon = Icon.FromHandle(Resource.settings.GetHicon());

            delayValue.Text = Properties.Settings.Default.Delay.ToString();
            autorun.Checked = Properties.Settings.Default.Autorun;
            passwordCheck.Checked = Properties.Settings.Default.PasswordCheck;
            password.Text = Properties.Settings.Default.Password;
        }

        private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
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
            Properties.Settings.Default.Delay = Convert.ToInt32(delayValue.Text);
            Properties.Settings.Default.Autorun = autorun.Checked;
            Properties.Settings.Default.PasswordCheck = passwordCheck.Checked;
            Properties.Settings.Default.Password = password.Text;


            RegistryKey cukey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            if (autorun.Checked)
            {

                cukey.SetValue("PCShutdown", Directory.GetCurrentDirectory() + "\\" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".exe");
            }
            else
            {
                if (cukey.GetValue("PCShutdown") != null)
                    cukey.DeleteValue("PCShutdown");
            }
            Properties.Settings.Default.Save();
            this.Hide();
            ShutdownApp.ShowBallon("Настройки сохранены!", ToolTipIcon.Info);
        }

        private void showPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (showPassword.Checked)
                password.UseSystemPasswordChar = false;
            else
                password.UseSystemPasswordChar = true;
        }
    }
}
