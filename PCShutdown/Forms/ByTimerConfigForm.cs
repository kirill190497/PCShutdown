using System;
using System.Drawing;
using System.Windows.Forms;
using BlueMystic;
using PCShutdown.Classes;

namespace PCShutdown.Forms
{
    public partial class ByTimerConfigForm : Form
    {
        private readonly Strings S = ShutdownApp.Translation.Lang.Strings;

        public ByTimerConfigForm()
        {
            InitializeComponent();
            ApplyTranslation();
            _ = new DarkModeCS(this);
            Icon = Icon.FromHandle(Resource.hourglass.GetHicon());
            command.Items.AddRange(new object[] { S.ShutdownPC, S.RebootPC, S.Sleep, S.Hibernation, S.LockScreen, S.Notification, S.TurnOffScreen, S.TurnOnScreen });
            period.Items.AddRange(new object[] { S.Minutes, S.Hours});
            command.SelectedIndex = 0;
            period.SelectedIndex = 0;
            CalculateRemainedTime();
            
        }

        

        private void ApplyTranslation()
        {
            Text = "PCShutdown - " + S.AddByTimerButton;

            Save.Text = S.SaveButton;
            command_label.Text = S.Action;
            comment_label.Text = S.TaskMessage;
            after_label.Text = S.AfterLabel;
            exectime_label.Text = S.ExecTime;

        }

        private int ValueToSeconds(int value, string period)
        {
            
                if (period == S.Minutes) return (int)(value * 60);
                if (period == S.Hours) return (int)(value * 60 * 60);
                return 0;

        }

        private DateTime CalculateRemainedTime()
        {
            int seconds = ValueToSeconds((int)value.Value, period.SelectedItem.ToString());
            DateTime now = DateTime.Now;

            var date = now.AddSeconds(seconds);
            if (date <= now)
                remained_label.Text = "Now";
            else
                remained_label.Text = date.ToString("H:m dd.MM.yy");
            return date;
        }


        private ShutdownTask.TaskType ParseCommand(string command)
        {
            ShutdownTask.TaskType parsed = ShutdownTask.TaskType.None;

            
            if (command == S.ShutdownPC) parsed = ShutdownTask.TaskType.ShutdownPC;
            if (command == S.RebootPC) parsed = ShutdownTask.TaskType.RebootPC;
            if (command == S.Sleep) parsed = ShutdownTask.TaskType.Sleep;
            if (command == S.Hibernation) parsed = ShutdownTask.TaskType.Hibernaiton;
            if (command == S.LockScreen) parsed = ShutdownTask.TaskType.Lock;
            if (command == S.TurnOffScreen) parsed = ShutdownTask.TaskType.ScreenOff;
            if (command == S.TurnOnScreen) parsed = ShutdownTask.TaskType.ScreenOn;
            if (command == S.Notification) parsed = ShutdownTask.TaskType.Notification; 
               
            

            return parsed;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            DateTime date = CalculateRemainedTime();
            ShutdownTask.TaskType parsed = ParseCommand(command.SelectedItem.ToString());
            Server.AddTask(parsed, date, comment.Text);
            ShutdownApp.ShowToast(command.SelectedItem.ToString() + " - " + date + "\n"+ S.TaskMessage+": " + comment.Text, S.TaskAdded);
            ShutdownApp.UpdateTasksListForm();


        }

        private void period_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateRemainedTime();
        }

        private void value_ValueChanged(object sender, EventArgs e)
        {
            CalculateRemainedTime();
        }

        private void ByTimerConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           /* if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }*/
        }
    }
}
