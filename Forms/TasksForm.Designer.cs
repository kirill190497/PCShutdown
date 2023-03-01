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
            this.TaskList = new System.Windows.Forms.ListView();
            this.TaskID = new System.Windows.Forms.ColumnHeader();
            this.TaskType = new System.Windows.Forms.ColumnHeader();
            this.TaskTime = new System.Windows.Forms.ColumnHeader();
            this.TaskMessage = new System.Windows.Forms.ColumnHeader();
            this.UpdateListButton = new System.Windows.Forms.Button();
            this.AddTaskGroup = new System.Windows.Forms.GroupBox();
            this.AddInTime = new System.Windows.Forms.Button();
            this.AddByTimer = new System.Windows.Forms.Button();
            this.DeleteTaskGroup = new System.Windows.Forms.GroupBox();
            this.RemoveAll = new System.Windows.Forms.Button();
            this.RemoveSelected = new System.Windows.Forms.Button();
            this.NoteGroup = new System.Windows.Forms.GroupBox();
            this.noteTextBox = new System.Windows.Forms.TextBox();
            this.AddTaskGroup.SuspendLayout();
            this.DeleteTaskGroup.SuspendLayout();
            this.NoteGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // TaskList
            // 
            this.TaskList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TaskID,
            this.TaskType,
            this.TaskTime,
            this.TaskMessage});
            this.TaskList.FullRowSelect = true;
            this.TaskList.GridLines = true;
            listViewGroup1.Footer = "1";
            listViewGroup1.Header = "ListViewGroup";
            listViewGroup1.Name = "listViewGroup1";
            this.TaskList.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            this.TaskList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.TaskList.Location = new System.Drawing.Point(12, 12);
            this.TaskList.Name = "TaskList";
            this.TaskList.ShowGroups = false;
            this.TaskList.Size = new System.Drawing.Size(471, 389);
            this.TaskList.TabIndex = 0;
            this.TaskList.UseCompatibleStateImageBehavior = false;
            this.TaskList.View = System.Windows.Forms.View.Details;
            // 
            // TaskID
            // 
            this.TaskID.Text = "ID";
            // 
            // TaskType
            // 
            this.TaskType.Tag = "type";
            this.TaskType.Text = "Задача";
            this.TaskType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TaskType.Width = 120;
            // 
            // TaskTime
            // 
            this.TaskTime.Text = "Время";
            this.TaskTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TaskTime.Width = 120;
            // 
            // TaskMessage
            // 
            this.TaskMessage.Text = "Комментарий";
            this.TaskMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TaskMessage.Width = 160;
            // 
            // UpdateListButton
            // 
            this.UpdateListButton.Location = new System.Drawing.Point(489, 378);
            this.UpdateListButton.Name = "UpdateListButton";
            this.UpdateListButton.Size = new System.Drawing.Size(210, 23);
            this.UpdateListButton.TabIndex = 1;
            this.UpdateListButton.Text = "Обновить";
            this.UpdateListButton.UseVisualStyleBackColor = true;
            this.UpdateListButton.Click += new System.EventHandler(this.UpdateListButton_Click);
            // 
            // AddTaskGroup
            // 
            this.AddTaskGroup.Controls.Add(this.AddInTime);
            this.AddTaskGroup.Controls.Add(this.AddByTimer);
            this.AddTaskGroup.Location = new System.Drawing.Point(489, 12);
            this.AddTaskGroup.Name = "AddTaskGroup";
            this.AddTaskGroup.Size = new System.Drawing.Size(210, 57);
            this.AddTaskGroup.TabIndex = 2;
            this.AddTaskGroup.TabStop = false;
            this.AddTaskGroup.Text = "Добавить задачу";
            // 
            // AddInTime
            // 
            this.AddInTime.Location = new System.Drawing.Point(114, 22);
            this.AddInTime.Name = "AddInTime";
            this.AddInTime.Size = new System.Drawing.Size(90, 23);
            this.AddInTime.TabIndex = 1;
            this.AddInTime.Text = "По времени";
            this.AddInTime.UseVisualStyleBackColor = true;
            this.AddInTime.Click += new System.EventHandler(this.AddInTime_Click);
            // 
            // AddByTimer
            // 
            this.AddByTimer.Location = new System.Drawing.Point(6, 22);
            this.AddByTimer.Name = "AddByTimer";
            this.AddByTimer.Size = new System.Drawing.Size(90, 23);
            this.AddByTimer.TabIndex = 0;
            this.AddByTimer.Text = "По таймеру";
            this.AddByTimer.UseVisualStyleBackColor = true;
            this.AddByTimer.Click += new System.EventHandler(this.AddByTimer_Click);
            // 
            // DeleteTaskGroup
            // 
            this.DeleteTaskGroup.Controls.Add(this.RemoveAll);
            this.DeleteTaskGroup.Controls.Add(this.RemoveSelected);
            this.DeleteTaskGroup.Location = new System.Drawing.Point(489, 209);
            this.DeleteTaskGroup.Name = "DeleteTaskGroup";
            this.DeleteTaskGroup.Size = new System.Drawing.Size(210, 57);
            this.DeleteTaskGroup.TabIndex = 3;
            this.DeleteTaskGroup.TabStop = false;
            this.DeleteTaskGroup.Text = "Удалить задачу";
            // 
            // RemoveAll
            // 
            this.RemoveAll.Location = new System.Drawing.Point(114, 22);
            this.RemoveAll.Name = "RemoveAll";
            this.RemoveAll.Size = new System.Drawing.Size(90, 23);
            this.RemoveAll.TabIndex = 1;
            this.RemoveAll.Text = "Все";
            this.RemoveAll.UseVisualStyleBackColor = true;
            this.RemoveAll.Click += new System.EventHandler(this.RemoveAll_Click);
            // 
            // RemoveSelected
            // 
            this.RemoveSelected.Location = new System.Drawing.Point(6, 22);
            this.RemoveSelected.Name = "RemoveSelected";
            this.RemoveSelected.Size = new System.Drawing.Size(90, 23);
            this.RemoveSelected.TabIndex = 0;
            this.RemoveSelected.Text = "Выбранные";
            this.RemoveSelected.UseVisualStyleBackColor = true;
            this.RemoveSelected.Click += new System.EventHandler(this.RemoveSelected_Click);
            // 
            // NoteGroup
            // 
            this.NoteGroup.Controls.Add(this.noteTextBox);
            this.NoteGroup.Location = new System.Drawing.Point(489, 272);
            this.NoteGroup.Name = "NoteGroup";
            this.NoteGroup.Size = new System.Drawing.Size(210, 100);
            this.NoteGroup.TabIndex = 4;
            this.NoteGroup.TabStop = false;
            this.NoteGroup.Text = "Примечание";
            // 
            // noteTextBox
            // 
            this.noteTextBox.Location = new System.Drawing.Point(6, 22);
            this.noteTextBox.Multiline = true;
            this.noteTextBox.Name = "noteTextBox";
            this.noteTextBox.ReadOnly = true;
            this.noteTextBox.Size = new System.Drawing.Size(198, 72);
            this.noteTextBox.TabIndex = 0;
            // 
            // TasksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 413);
            this.Controls.Add(this.NoteGroup);
            this.Controls.Add(this.DeleteTaskGroup);
            this.Controls.Add(this.AddTaskGroup);
            this.Controls.Add(this.UpdateListButton);
            this.Controls.Add(this.TaskList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TasksForm";
            this.Text = "TasksForm";
            this.Activated += new System.EventHandler(this.TasksForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TasksForm_FormClosing);
            this.Load += new System.EventHandler(this.TasksForm_Load);
            this.AddTaskGroup.ResumeLayout(false);
            this.DeleteTaskGroup.ResumeLayout(false);
            this.NoteGroup.ResumeLayout(false);
            this.NoteGroup.PerformLayout();
            this.ResumeLayout(false);

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