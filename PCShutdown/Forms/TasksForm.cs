using System;
using System.Drawing;
using System.Windows.Forms;

using PCShutdown.Classes;

namespace PCShutdown.Forms
{
    public partial class TasksForm : Form
    {
        private readonly Strings S = ShutdownApp.Translation.Lang.Strings;
        public TasksForm()
        {
            InitializeComponent();
            
            this.Icon = Icon.FromHandle(Resource.tasks.GetHicon());

            ApplyTranslation();
        }

        private void ApplyTranslation()
        {
            this.Text = "PCShutdown - " + S.Tasks;
            TaskTime.Text = S.TaskTime;
            TaskType.Text = S.TaskType;
            TaskMessage.Text = S.TaskMessage;
            AddTaskGroup.Text = S.AddTaskGroup;
            DeleteTaskGroup.Text = S.DeleteTaskGroup;
            AddInTime.Text = S.AddInTimeButton;
            AddByTimer.Text = S.AddByTimerButton;
            RemoveAll.Text = S.RemoveAllButton;
            RemoveSelected.Text = S.RemoveSelectedButton;
            NoteGroup.Text = S.NoteGroup;
            noteTextBox.Text = S.NoteText;
            UpdateListButton.Text = S.UpdateListButton;

        }

        public void UpdateList()
        {
            TaskList.Items.Clear();
            var tasks = Server.GetTasksList();

            foreach (var task in tasks)
            {
                string[] item = { task.ID.ToString(), task.GetTranslatedTypeName(), task.Date.ToString(), task.Comment };
                TaskList.Items.Add(new ListViewItem(item));
            }
        }

        private void TasksForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void TasksForm_Load(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void UpdateListButton_Click(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void AddByTimer_Click(object sender, EventArgs e)
        {
            ShutdownApp.Forms.ShowForm(typeof(ByTimerConfigForm));

        }

        private void AddInTime_Click(object sender, EventArgs e)
        {
            ShutdownApp.Forms.ShowForm(typeof(InTimeConfigForm));
        }

        private void RemoveSelected_Click(object sender, EventArgs e)
        {
            if (TaskList.SelectedItems.Count > 0)
            {
                var selected = TaskList.SelectedItems;
                foreach (ListViewItem sel in selected)
                {
                    Server.RemoveTaskByID(int.Parse(sel.Text));
                }
                ShutdownApp.ShowToast(S.SelectedTasksRemoved);
                UpdateList();
            }
            else
            {
                ShutdownApp.ShowToast(S.TaskNotSelected);
            }


        }

        private void RemoveAll_Click(object sender, EventArgs e)
        {
            Server.RemoveAllTasks();
            UpdateList();
            ShutdownApp.ShowToast(S.QueueCleared, S.TasksCanceled);
        }

        private void TasksForm_Activated(object sender, EventArgs e)
        {
            UpdateList();
        }
    }
}
