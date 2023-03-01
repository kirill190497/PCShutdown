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
            this.command_label = new System.Windows.Forms.Label();
            this.command = new System.Windows.Forms.ComboBox();
            this.remained_label = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.date_label = new System.Windows.Forms.Label();
            this.save_button = new System.Windows.Forms.Button();
            this.comment = new System.Windows.Forms.TextBox();
            this.comment_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // command_label
            // 
            this.command_label.AutoSize = true;
            this.command_label.Location = new System.Drawing.Point(14, 37);
            this.command_label.Name = "command_label";
            this.command_label.Size = new System.Drawing.Size(71, 20);
            this.command_label.TabIndex = 0;
            this.command_label.Text = "Команда";
            // 
            // command
            // 
            this.command.FormattingEnabled = true;
            this.command.Location = new System.Drawing.Point(172, 34);
            this.command.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.command.Name = "command";
            this.command.Size = new System.Drawing.Size(217, 28);
            this.command.TabIndex = 1;
            this.command.SelectedIndexChanged += new System.EventHandler(this.command_SelectedIndexChanged);
            // 
            // remained_label
            // 
            this.remained_label.AutoSize = true;
            this.remained_label.Location = new System.Drawing.Point(153, 85);
            this.remained_label.Name = "remained_label";
            this.remained_label.Size = new System.Drawing.Size(94, 15);
            this.remained_label.TabIndex = 8;
            this.remained_label.Text = "00:00 01.01.1970 ";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(172, 73);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(217, 27);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // date_label
            // 
            this.date_label.AutoSize = true;
            this.date_label.Location = new System.Drawing.Point(14, 80);
            this.date_label.Name = "date_label";
            this.date_label.Size = new System.Drawing.Size(41, 20);
            this.date_label.TabIndex = 3;
            this.date_label.Text = "Дата";
            // 
            // save_button
            // 
            this.save_button.Location = new System.Drawing.Point(7, 284);
            this.save_button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(382, 31);
            this.save_button.TabIndex = 4;
            this.save_button.Text = "Сохранить";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // comment
            // 
            this.comment.Location = new System.Drawing.Point(14, 143);
            this.comment.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comment.Multiline = true;
            this.comment.Name = "comment";
            this.comment.Size = new System.Drawing.Size(375, 93);
            this.comment.TabIndex = 12;
            // 
            // comment_label
            // 
            this.comment_label.AutoSize = true;
            this.comment_label.Location = new System.Drawing.Point(14, 119);
            this.comment_label.Name = "comment_label";
            this.comment_label.Size = new System.Drawing.Size(110, 20);
            this.comment_label.TabIndex = 11;
            this.comment_label.Text = "Комментарий:";
            // 
            // InTimeConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 331);
            this.Controls.Add(this.comment);
            this.Controls.Add(this.comment_label);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.date_label);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.command);
            this.Controls.Add(this.command_label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InTimeConfigForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "InTimeConfigForm";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InTimeConfigForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

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