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
            this.command = new System.Windows.Forms.ComboBox();
            this.Save = new System.Windows.Forms.Button();
            this.command_label = new System.Windows.Forms.Label();
            this.after_label = new System.Windows.Forms.Label();
            this.value = new System.Windows.Forms.NumericUpDown();
            this.period = new System.Windows.Forms.ComboBox();
            this.exectime_label = new System.Windows.Forms.Label();
            this.remained_label = new System.Windows.Forms.Label();
            this.comment_label = new System.Windows.Forms.Label();
            this.comment = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.value)).BeginInit();
            this.SuspendLayout();
            // 
            // command
            // 
            this.command.FormattingEnabled = true;
            this.command.Location = new System.Drawing.Point(99, 12);
            this.command.Name = "command";
            this.command.Size = new System.Drawing.Size(168, 23);
            this.command.TabIndex = 0;
            // 
            // Save
            // 
            this.Save.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Save.AutoSize = true;
            this.Save.Location = new System.Drawing.Point(12, 188);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(255, 41);
            this.Save.TabIndex = 1;
            this.Save.Text = "Сохранить";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // command_label
            // 
            this.command_label.AutoSize = true;
            this.command_label.Location = new System.Drawing.Point(12, 15);
            this.command_label.Name = "command_label";
            this.command_label.Size = new System.Drawing.Size(55, 15);
            this.command_label.TabIndex = 2;
            this.command_label.Text = "Команда";
            // 
            // after_label
            // 
            this.after_label.AutoSize = true;
            this.after_label.Location = new System.Drawing.Point(12, 42);
            this.after_label.Name = "after_label";
            this.after_label.Size = new System.Drawing.Size(45, 15);
            this.after_label.TabIndex = 4;
            this.after_label.Text = "Через: ";
            // 
            // value
            // 
            this.value.Location = new System.Drawing.Point(99, 40);
            this.value.Name = "value";
            this.value.Size = new System.Drawing.Size(65, 23);
            this.value.TabIndex = 5;
            this.value.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.value.ValueChanged += new System.EventHandler(this.value_ValueChanged);
            // 
            // period
            // 
            this.period.FormattingEnabled = true;
            this.period.Location = new System.Drawing.Point(170, 39);
            this.period.Name = "period";
            this.period.Size = new System.Drawing.Size(97, 23);
            this.period.TabIndex = 6;
            this.period.SelectedIndexChanged += new System.EventHandler(this.period_SelectedIndexChanged);
            // 
            // exectime_label
            // 
            this.exectime_label.AutoSize = true;
            this.exectime_label.Location = new System.Drawing.Point(15, 170);
            this.exectime_label.Name = "exectime_label";
            this.exectime_label.Size = new System.Drawing.Size(108, 15);
            this.exectime_label.TabIndex = 7;
            this.exectime_label.Text = "Будет выполнена: ";
            // 
            // remained_label
            // 
            this.remained_label.AutoSize = true;
            this.remained_label.Location = new System.Drawing.Point(156, 170);
            this.remained_label.Name = "remained_label";
            this.remained_label.Size = new System.Drawing.Size(94, 15);
            this.remained_label.TabIndex = 8;
            this.remained_label.Text = "00:00 01.01.1970 ";
            // 
            // comment_label
            // 
            this.comment_label.AutoSize = true;
            this.comment_label.Location = new System.Drawing.Point(12, 78);
            this.comment_label.Name = "comment_label";
            this.comment_label.Size = new System.Drawing.Size(87, 15);
            this.comment_label.TabIndex = 9;
            this.comment_label.Text = "Комментарий:";
            // 
            // comment
            // 
            this.comment.Location = new System.Drawing.Point(12, 96);
            this.comment.Multiline = true;
            this.comment.Name = "comment";
            this.comment.Size = new System.Drawing.Size(255, 71);
            this.comment.TabIndex = 10;
            // 
            // ByTimerConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 241);
            this.Controls.Add(this.comment);
            this.Controls.Add(this.comment_label);
            this.Controls.Add(this.remained_label);
            this.Controls.Add(this.exectime_label);
            this.Controls.Add(this.period);
            this.Controls.Add(this.value);
            this.Controls.Add(this.after_label);
            this.Controls.Add(this.command_label);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.command);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ByTimerConfigForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Выполнить по таймеру";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ByTimerConfigForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.value)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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