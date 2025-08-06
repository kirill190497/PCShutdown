namespace PCShutdown.Forms
{
    partial class ByTimerConfigForm
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
            command = new System.Windows.Forms.ComboBox();
            Save = new System.Windows.Forms.Button();
            command_label = new System.Windows.Forms.Label();
            after_label = new System.Windows.Forms.Label();
            value = new System.Windows.Forms.NumericUpDown();
            period = new System.Windows.Forms.ComboBox();
            exectime_label = new System.Windows.Forms.Label();
            remained_label = new System.Windows.Forms.Label();
            comment_label = new System.Windows.Forms.Label();
            comment = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)value).BeginInit();
            SuspendLayout();
            // 
            // command
            // 
            command.FormattingEnabled = true;
            command.Location = new System.Drawing.Point(99, 12);
            command.Name = "command";
            command.Size = new System.Drawing.Size(168, 23);
            command.TabIndex = 0;
            // 
            // Save
            // 
            Save.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            Save.AutoSize = true;
            Save.Location = new System.Drawing.Point(12, 188);
            Save.Name = "Save";
            Save.Size = new System.Drawing.Size(255, 41);
            Save.TabIndex = 1;
            Save.Text = "Сохранить";
            Save.UseVisualStyleBackColor = true;
            Save.Click += Save_Click;
            // 
            // command_label
            // 
            command_label.AutoSize = true;
            command_label.Location = new System.Drawing.Point(12, 15);
            command_label.Name = "command_label";
            command_label.Size = new System.Drawing.Size(55, 15);
            command_label.TabIndex = 2;
            command_label.Text = "Команда";
            // 
            // after_label
            // 
            after_label.AutoSize = true;
            after_label.Location = new System.Drawing.Point(12, 42);
            after_label.Name = "after_label";
            after_label.Size = new System.Drawing.Size(45, 15);
            after_label.TabIndex = 4;
            after_label.Text = "Через: ";
            // 
            // value
            // 
            value.Location = new System.Drawing.Point(99, 40);
            value.Name = "value";
            value.Size = new System.Drawing.Size(65, 23);
            value.TabIndex = 5;
            value.Value = new decimal(new int[] { 10, 0, 0, 0 });
            value.ValueChanged += value_ValueChanged;
            // 
            // period
            // 
            period.FormattingEnabled = true;
            period.Location = new System.Drawing.Point(170, 39);
            period.Name = "period";
            period.Size = new System.Drawing.Size(97, 23);
            period.TabIndex = 6;
            period.SelectedIndexChanged += period_SelectedIndexChanged;
            // 
            // exectime_label
            // 
            exectime_label.AutoSize = true;
            exectime_label.Location = new System.Drawing.Point(15, 170);
            exectime_label.Name = "exectime_label";
            exectime_label.Size = new System.Drawing.Size(108, 15);
            exectime_label.TabIndex = 7;
            exectime_label.Text = "Будет выполнена: ";
            // 
            // remained_label
            // 
            remained_label.AutoSize = true;
            remained_label.Location = new System.Drawing.Point(156, 170);
            remained_label.Name = "remained_label";
            remained_label.Size = new System.Drawing.Size(94, 15);
            remained_label.TabIndex = 8;
            remained_label.Text = "00:00 01.01.1970 ";
            // 
            // comment_label
            // 
            comment_label.AutoSize = true;
            comment_label.Location = new System.Drawing.Point(12, 78);
            comment_label.Name = "comment_label";
            comment_label.Size = new System.Drawing.Size(87, 15);
            comment_label.TabIndex = 9;
            comment_label.Text = "Комментарий:";
            // 
            // comment
            // 
            comment.Location = new System.Drawing.Point(12, 96);
            comment.Multiline = true;
            comment.Name = "comment";
            comment.Size = new System.Drawing.Size(255, 71);
            comment.TabIndex = 10;
            // 
            // ByTimerConfigForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(279, 241);
            Controls.Add(comment);
            Controls.Add(comment_label);
            Controls.Add(remained_label);
            Controls.Add(exectime_label);
            Controls.Add(period);
            Controls.Add(value);
            Controls.Add(after_label);
            Controls.Add(command_label);
            Controls.Add(Save);
            Controls.Add(command);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ByTimerConfigForm";
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Выполнить по таймеру";
            TopMost = true;
            FormClosing += ByTimerConfigForm_FormClosing;
            ((System.ComponentModel.ISupportInitialize)value).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox command;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Label command_label;
        private System.Windows.Forms.Label after_label;
        private System.Windows.Forms.NumericUpDown value;
        private System.Windows.Forms.ComboBox period;
        private System.Windows.Forms.Label exectime_label;
        private System.Windows.Forms.Label remained_label;
        private System.Windows.Forms.Label comment_label;
        private System.Windows.Forms.TextBox comment;
    }
}