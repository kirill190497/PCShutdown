using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PCShutdown
{
    public partial class ByTimerConfigForm : Form
    {
        
        public ByTimerConfigForm()
        {
            InitializeComponent();
            Text = "PCShutdown - Выполнить по таймеру";
            Icon = Icon.FromHandle(Resource.hourglass.GetHicon());
            command.Items.AddRange(new object[] { "Выключить", "Перезагрузить" }); // "Сон", "Гибернация"
            period.Items.AddRange(new object[] {"Минуты", "Часы"});
            command.SelectedIndex = 0;
            period.SelectedIndex = 0;

            

            CalculateRemainedTime();
        }



        private int ValueToSeconds(int value, string period)
        {
            switch (period)
            {
                case "Минуты": return (int)(value * 60);
                case "Часы": return (int)(value * 60 * 60);
                default: return 0;
            }
            
        }

        private void CalculateRemainedTime()
        {
            int seconds = ValueToSeconds((int)value.Value, period.SelectedItem.ToString());
            DateTime date = DateTime.Now;

            date = date.AddSeconds(seconds);
            remained_label.Text = date.ToString("H:m d.M.yy");
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

        private void Save_Click(object sender, EventArgs e)
        {

            int seconds = ValueToSeconds((int)value.Value, period.SelectedItem.ToString());
            string parsed = ParseCommand(command.SelectedItem.ToString());

            Server.execCommand(parsed, seconds);

        }

        private void period_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateRemainedTime();
        }

        private void value_ValueChanged(object sender, EventArgs e)
        {
            CalculateRemainedTime();
        }
    }
}
