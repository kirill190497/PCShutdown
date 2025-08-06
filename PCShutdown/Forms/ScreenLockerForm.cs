
using PCShutdown.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PCShutdown.Forms
{
    public partial class ScreenLockerForm : Form
    {
        private bool Unlocked = false;
        private string Pin { get; set; }
        public ScreenLocker Locker { get; set; }
        private readonly Strings S = ShutdownApp.Translation.Lang.Strings;
        public ScreenLockerForm(string pin = "")
        {
            this.TransparencyKey = Color.AliceBlue;

            InitializeComponent();

            Locker = new ScreenLocker(pin);
            Pin = pin;
            var bounds = Screen.PrimaryScreen.Bounds;
            LockedLabel.AutoSize = true;
            panel1.AutoSize = true;
            unlockButton.AutoSize = true;
            LockedLabel.Text = S.ScreenLocked;
            pincode.PlaceholderText = S.EnterPinLabel;
            unlockButton.Text = S.UnlockButton;
            TopMost = true;
            AutoSize = true;
            KeyPreview = true;
            panel1.BackColor = Color.Transparent;
            panel1.Location = new Point(bounds.Width / 2 - panel1.Width / 2, bounds.Height / 2 - panel1.Height / 2);
            ErrorLabel.BackColor = Color.Transparent;
            LockedLabel.BackColor = Color.Transparent;
            LockedLabel.ForeColor = Color.Red;
            ErrorLabel.Visible = false;
            pincode.UseSystemPasswordChar = true;


            unlockButton.Size = new Size(LockedLabel.Width / 6, pincode.Height);
            pincode.Size = new Size(LockedLabel.Width / 2 - unlockButton.Width - 10, pincode.Height);
            pincode.Location = new Point(LockedLabel.Location.X + (LockedLabel.Width / 4), LockedLabel.Location.Y + LockedLabel.Height);
            unlockButton.Location = new Point(pincode.Location.X + pincode.Width + 10, pincode.Location.Y);


        }




        private void ScreenLockerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Unlocked)
                e.Cancel = true;
        }

        public bool Unlock(string pin = "")
        {
            if (pin != "")
            {
                pincode.Text = pin;
            }
            Unlocked = Locker.TryUnlock(pincode.Text);
            if (Unlocked)
            {
                Close();
            }
            else
            {
                ShowMessage(S.WrongPassword);
                pincode.ForeColor = Color.Red;
            }
            return Unlocked;
        }

        private void ShowMessage(string text, bool error = true)
        {
            ErrorLabel.Text = text;
            ErrorLabel.Visible = true;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Unlock();
        }

        private void ScreenLockerForm_KeyDown(object sender, KeyEventArgs e)
        {
            List<Keys> lockedKeys = new()
            {
                Keys.LWin, Keys.RWin, Keys.Space, Keys.Escape, Keys.End, Keys.PrintScreen, Keys.Tab, Keys.Menu, Keys.Shift, Keys.Control, Keys.Alt, Keys.ControlKey, Keys.LControlKey, Keys.RControlKey, Keys.F4
            };
            if (lockedKeys.Contains(e.KeyCode))
            {
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (pincode.Focused)
                {
                    Unlock();
                }
            }
            else
            {
                if (!pincode.Focused)
                {

                    pincode.Focus();
                    pincode.Text += e.KeyCode;
                    pincode.SelectionStart = pincode.TextLength;

                }

            }
        }

        private void ScreenLockerForm_Paint(object sender, PaintEventArgs e)
        {
            var hatchBrush = new HatchBrush(HatchStyle.Percent30, this.TransparencyKey);
            e.Graphics.FillRectangle(hatchBrush, this.DisplayRectangle);

        }

        private void LockedLabel_Paint(object sender, PaintEventArgs e)
        {

        }


        private void pincode_TextChanged(object sender, EventArgs e)
        {
            pincode.ForeColor = Color.Black;
            ErrorLabel.Visible = false;
            var textBox = (TextBox)sender;
            string text = textBox.Text;
            for (int i = 0; i < text.Length; i++)
            {
                if (!char.IsDigit(text[i]))
                {
                    //MessageBox.Show(text[i].ToString());
                    text = text.Replace(text[i].ToString(), "");
                    pincode.Text = text;
                }

            }
        }

        private void pincode_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var hatchBrush = new HatchBrush(HatchStyle.Percent30, this.TransparencyKey);
            e.Graphics.FillRectangle(hatchBrush, this.DisplayRectangle);
        }

        private void ScreenLockerForm_Deactivate(object sender, EventArgs e)
        {

            pincode.Focus();
            
        }
    }
}
