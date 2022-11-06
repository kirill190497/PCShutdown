namespace PCShutdown
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.value = new System.Windows.Forms.NumericUpDown();
            this.period = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.remained_label = new System.Windows.Forms.Label();
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
            this.Save.Location = new System.Drawing.Point(12, 128);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(255, 34);
            this.Save.TabIndex = 1;
            this.Save.Text = "Сохранить";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Команда";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Через: ";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Будет выполнена: ";
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
            // ByTimerConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 191);
            this.Controls.Add(this.remained_label);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.period);
            this.Controls.Add(this.value);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.command);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ByTimerConfigForm";
            this.Text = "Выполнить по таймеру";
            ((System.ComponentModel.ISupportInitialize)(this.value)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox command;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown value;
        private System.Windows.Forms.ComboBox period;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label remained_label;
    }
}