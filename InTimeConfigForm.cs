using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PCShutdown
{
    public partial class InTimeConfigForm : Form
    {
        public InTimeConfigForm()
        {
            InitializeComponent();
            Text = "PCShutdown - По времени";
            Icon = Icon.FromHandle(Resource.timer.GetHicon());
            command.Items.AddRange(new object[] { "Выключить", "Перезагрузить" }); // "Сон", "Гибернация"
            command.SelectedIndex = 0;
            dateTimePicker1.CustomFormat = "HH:mm dd.MM.yy";
        }

        private void command_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private string ParseCommand(string command)
        {
            string parsed;

            switch (command)
            {
                case "Выключить": parsed = "shutdown"; break;
                case "Перезагрузить": parsed = "reboot"; break;
                case "Сон": parsed = "sleep"; break;
                case "Гибернация": parsed = "hibernate"; break;

                default: parsed = ""; break;
            }

            return parsed;
        }

        private void save_button_Click(object sender, EventArgs e)
        {

            DateTime date_now = DateTime.Now;
            DateTime date_command = dateTimePicker1.Value;

            int seconds = (int)(date_command - date_now).TotalSeconds;

            string parsed = ParseCommand(command.SelectedItem.ToString());

            Server.execCommand(parsed, seconds);

        }

       
    }
}
