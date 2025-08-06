namespace PCShutdown.Forms
{
    partial class InTimeConfigForm
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
            command_label = new System.Windows.Forms.Label();
            command = new System.Windows.Forms.ComboBox();
            remained_label = new System.Windows.Forms.Label();
            dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            date_label = new System.Windows.Forms.Label();
            save_button = new System.Windows.Forms.Button();
            comment = new System.Windows.Forms.TextBox();
            comment_label = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // command_label
            // 
            command_label.AutoSize = true;
            command_label.Location = new System.Drawing.Point(12, 28);
            command_label.Name = "command_label";
            command_label.Size = new System.Drawing.Size(55, 15);
            command_label.TabIndex = 0;
            command_label.Text = "Команда";
            // 
            // command
            // 
            command.FormattingEnabled = true;
            command.Location = new System.Drawing.Point(150, 26);
            command.Name = "command";
            command.Size = new System.Drawing.Size(190, 23);
            command.TabIndex = 1;
            command.SelectedIndexChanged += command_SelectedIndexChanged;
            // 
            // remained_label
            // 
            remained_label.AutoSize = true;
            remained_label.Location = new System.Drawing.Point(153, 85);
            remained_label.Name = "remained_label";
            remained_label.Size = new System.Drawing.Size(94, 15);
            remained_label.TabIndex = 8;
            remained_label.Text = "00:00 01.01.1970 ";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new System.Drawing.Point(150, 55);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new System.Drawing.Size(190, 23);
            dateTimePicker1.TabIndex = 2;
            // 
            // date_label
            // 
            date_label.AutoSize = true;
            date_label.Location = new System.Drawing.Point(12, 60);
            date_label.Name = "date_label";
            date_label.Size = new System.Drawing.Size(32, 15);
            date_label.TabIndex = 3;
            date_label.Text = "Дата";
            // 
            // save_button
            // 
            save_button.Location = new System.Drawing.Point(6, 213);
            save_button.Name = "save_button";
            save_button.Size = new System.Drawing.Size(334, 23);
            save_button.TabIndex = 4;
            save_button.Text = "Сохранить";
            save_button.UseVisualStyleBackColor = true;
            save_button.Click += save_button_Click;
            // 
            // comment
            // 
            comment.Location = new System.Drawing.Point(12, 107);
            comment.Multiline = true;
            comment.Name = "comment";
            comment.Size = new System.Drawing.Size(329, 71);
            comment.TabIndex = 12;
            // 
            // comment_label
            // 
            comment_label.AutoSize = true;
            comment_label.Location = new System.Drawing.Point(12, 89);
            comment_label.Name = "comment_label";
            comment_label.Size = new System.Drawing.Size(87, 15);
            comment_label.TabIndex = 11;
            comment_label.Text = "Комментарий:";
            // 
            // InTimeConfigForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(351, 248);
            Controls.Add(comment);
            Controls.Add(comment_label);
            Controls.Add(save_button);
            Controls.Add(date_label);
            Controls.Add(dateTimePicker1);
            Controls.Add(command);
            Controls.Add(command_label);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InTimeConfigForm";
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "InTimeConfigForm";
            TopMost = true;
            FormClosing += InTimeConfigForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label command_label;
        private System.Windows.Forms.ComboBox command;
        private System.Windows.Forms.Label remained_label;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label date_label;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.TextBox comment;
        private System.Windows.Forms.Label comment_label;
    }
}