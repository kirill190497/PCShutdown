namespace PCShutdown.Forms
{
    partial class TasksForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            TaskList = new System.Windows.Forms.ListView();
            TaskID = new System.Windows.Forms.ColumnHeader();
            TaskType = new System.Windows.Forms.ColumnHeader();
            TaskTime = new System.Windows.Forms.ColumnHeader();
            TaskMessage = new System.Windows.Forms.ColumnHeader();
            UpdateListButton = new System.Windows.Forms.Button();
            AddTaskGroup = new System.Windows.Forms.GroupBox();
            AddInTime = new System.Windows.Forms.Button();
            AddByTimer = new System.Windows.Forms.Button();
            DeleteTaskGroup = new System.Windows.Forms.GroupBox();
            RemoveAll = new System.Windows.Forms.Button();
            RemoveSelected = new System.Windows.Forms.Button();
            NoteGroup = new System.Windows.Forms.GroupBox();
            noteTextBox = new System.Windows.Forms.TextBox();
            AddTaskGroup.SuspendLayout();
            DeleteTaskGroup.SuspendLayout();
            NoteGroup.SuspendLayout();
            SuspendLayout();
            // 
            // TaskList
            // 
            TaskList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { TaskID, TaskType, TaskTime, TaskMessage });
            TaskList.FullRowSelect = true;
            TaskList.GridLines = true;
            listViewGroup1.Footer = "1";
            listViewGroup1.Header = "ListViewGroup";
            listViewGroup1.Name = "listViewGroup1";
            TaskList.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] { listViewGroup1 });
            TaskList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            TaskList.Location = new System.Drawing.Point(12, 12);
            TaskList.Name = "TaskList";
            TaskList.ShowGroups = false;
            TaskList.Size = new System.Drawing.Size(471, 389);
            TaskList.TabIndex = 0;
            TaskList.UseCompatibleStateImageBehavior = false;
            TaskList.View = System.Windows.Forms.View.Details;
            // 
            // TaskID
            // 
            TaskID.Text = "ID";
            // 
            // TaskType
            // 
            TaskType.Tag = "type";
            TaskType.Text = "Задача";
            TaskType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            TaskType.Width = 120;
            // 
            // TaskTime
            // 
            TaskTime.Text = "Время";
            TaskTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            TaskTime.Width = 120;
            // 
            // TaskMessage
            // 
            TaskMessage.Text = "Комментарий";
            TaskMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            TaskMessage.Width = 160;
            // 
            // UpdateListButton
            // 
            UpdateListButton.Location = new System.Drawing.Point(489, 378);
            UpdateListButton.Name = "UpdateListButton";
            UpdateListButton.Size = new System.Drawing.Size(210, 23);
            UpdateListButton.TabIndex = 1;
            UpdateListButton.Text = "Обновить";
            UpdateListButton.UseVisualStyleBackColor = true;
            UpdateListButton.Click += UpdateListButton_Click;
            // 
            // AddTaskGroup
            // 
            AddTaskGroup.Controls.Add(AddInTime);
            AddTaskGroup.Controls.Add(AddByTimer);
            AddTaskGroup.Location = new System.Drawing.Point(489, 12);
            AddTaskGroup.Name = "AddTaskGroup";
            AddTaskGroup.Size = new System.Drawing.Size(210, 57);
            AddTaskGroup.TabIndex = 2;
            AddTaskGroup.TabStop = false;
            AddTaskGroup.Text = "Добавить задачу";
            // 
            // AddInTime
            // 
            AddInTime.Location = new System.Drawing.Point(114, 22);
            AddInTime.Name = "AddInTime";
            AddInTime.Size = new System.Drawing.Size(90, 23);
            AddInTime.TabIndex = 1;
            AddInTime.Text = "По времени";
            AddInTime.UseVisualStyleBackColor = true;
            AddInTime.Click += AddInTime_Click;
            // 
            // AddByTimer
            // 
            AddByTimer.Location = new System.Drawing.Point(6, 22);
            AddByTimer.Name = "AddByTimer";
            AddByTimer.Size = new System.Drawing.Size(90, 23);
            AddByTimer.TabIndex = 0;
            AddByTimer.Text = "По таймеру";
            AddByTimer.UseVisualStyleBackColor = true;
            AddByTimer.Click += AddByTimer_Click;
            // 
            // DeleteTaskGroup
            // 
            DeleteTaskGroup.Controls.Add(RemoveAll);
            DeleteTaskGroup.Controls.Add(RemoveSelected);
            DeleteTaskGroup.Location = new System.Drawing.Point(489, 209);
            DeleteTaskGroup.Name = "DeleteTaskGroup";
            DeleteTaskGroup.Size = new System.Drawing.Size(210, 57);
            DeleteTaskGroup.TabIndex = 3;
            DeleteTaskGroup.TabStop = false;
            DeleteTaskGroup.Text = "Удалить задачу";
            // 
            // RemoveAll
            // 
            RemoveAll.Location = new System.Drawing.Point(114, 22);
            RemoveAll.Name = "RemoveAll";
            RemoveAll.Size = new System.Drawing.Size(90, 23);
            RemoveAll.TabIndex = 1;
            RemoveAll.Text = "Все";
            RemoveAll.UseVisualStyleBackColor = true;
            RemoveAll.Click += RemoveAll_Click;
            // 
            // RemoveSelected
            // 
            RemoveSelected.Location = new System.Drawing.Point(6, 22);
            RemoveSelected.Name = "RemoveSelected";
            RemoveSelected.Size = new System.Drawing.Size(90, 23);
            RemoveSelected.TabIndex = 0;
            RemoveSelected.Text = "Выбранные";
            RemoveSelected.UseVisualStyleBackColor = true;
            RemoveSelected.Click += RemoveSelected_Click;
            // 
            // NoteGroup
            // 
            NoteGroup.Controls.Add(noteTextBox);
            NoteGroup.Location = new System.Drawing.Point(489, 272);
            NoteGroup.Name = "NoteGroup";
            NoteGroup.Size = new System.Drawing.Size(210, 100);
            NoteGroup.TabIndex = 4;
            NoteGroup.TabStop = false;
            NoteGroup.Text = "Примечание";
            // 
            // noteTextBox
            // 
            noteTextBox.Location = new System.Drawing.Point(6, 22);
            noteTextBox.Multiline = true;
            noteTextBox.Name = "noteTextBox";
            noteTextBox.ReadOnly = true;
            noteTextBox.Size = new System.Drawing.Size(198, 72);
            noteTextBox.TabIndex = 0;
            // 
            // TasksForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(705, 413);
            Controls.Add(NoteGroup);
            Controls.Add(DeleteTaskGroup);
            Controls.Add(AddTaskGroup);
            Controls.Add(UpdateListButton);
            Controls.Add(TaskList);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Name = "TasksForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "TasksForm";
            Activated += TasksForm_Activated;
            FormClosing += TasksForm_FormClosing;
            Load += TasksForm_Load;
            AddTaskGroup.ResumeLayout(false);
            DeleteTaskGroup.ResumeLayout(false);
            NoteGroup.ResumeLayout(false);
            NoteGroup.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListView TaskList;
        private System.Windows.Forms.ColumnHeader TaskType;
        private System.Windows.Forms.ColumnHeader TaskTime;
        private System.Windows.Forms.ColumnHeader TaskID;
        private System.Windows.Forms.Button UpdateListButton;
        private System.Windows.Forms.GroupBox AddTaskGroup;
        private System.Windows.Forms.Button AddInTime;
        private System.Windows.Forms.Button AddByTimer;
        private System.Windows.Forms.GroupBox DeleteTaskGroup;
        private System.Windows.Forms.Button RemoveAll;
        private System.Windows.Forms.Button RemoveSelected;
        private System.Windows.Forms.GroupBox NoteGroup;
        private System.Windows.Forms.TextBox noteTextBox;
        private System.Windows.Forms.ColumnHeader TaskMessage;
    }
}