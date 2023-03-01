using System;
using System.Drawing;
using System.Windows.Forms;
using PCShutdown.Classes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PCShutdown.Forms
{
    public partial class InTimeConfigForm : Form
    {
        private readonly Strings S = ShutdownApp.Translation.Lang.Strings;
        public InTimeConfigForm()
        {
            InitializeComponent();
            
            Icon = Icon.FromHandle(Resource.timer.GetHicon());
            command.Items.AddRange(new object[] { S.ShutdownPC, S.RebootPC, S.Sleep, S.Hibernation, S.LockScreen, S.Notification, S.TurnOffScreen, S.TurnOnScreen });
            command.SelectedIndex = 0;
            dateTimePicker1.CustomFormat = "HH:mm dd.MM.yy";
            ApplyTranslation();
        }
        private void ApplyTranslation()
        {
            Text = "PCShutdown - " + S.AddInTimeButton;
            save_button.Text = S.SaveButton;
            command_label.Text = S.Action;
            comment_label.Text = S.TaskMessage;
            date_label.Text = S.ExecTime;


        }
        private void command_SelectedIndexChanged(object sender, EventArgs e)
        {

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


        private void save_button_Click(object sender, EventArgs e)
        {

            DateTime date_now = DateTime.Now;
            DateTime date_command = dateTimePicker1.Value;

            int seconds = (int)(date_command - date_now).TotalSeconds;

            if (seconds >= 60)
            {
                ShutdownTask.TaskType parsed = ParseCommand(command.SelectedItem.ToString());
                Server.AddTask(parsed, date_command, comment.Text);
                ShutdownApp.ShowToast(command.SelectedItem.ToString() + " - " + date_command + "\n" + S.TaskMessage + ": " + comment.Text, S.TaskAdded);
                ShutdownApp.UpdateTasksListForm();
            }
            else
            {
                MessageBox.Show(S.SmallIntervalError + "\n" + S.SetDifferentTime, S.ErrorOccurred, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void InTimeConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
