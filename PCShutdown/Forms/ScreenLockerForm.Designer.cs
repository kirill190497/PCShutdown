namespace PCShutdown.Forms
{
    partial class ScreenLockerForm
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
            unlockButton = new System.Windows.Forms.Button();
            LockedLabel = new System.Windows.Forms.Label();
            pincode = new System.Windows.Forms.TextBox();
            panel1 = new System.Windows.Forms.Panel();
            ErrorLabel = new System.Windows.Forms.Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // unlockButton
            // 
            unlockButton.Location = new System.Drawing.Point(352, 78);
            unlockButton.Name = "unlockButton";
            unlockButton.Size = new System.Drawing.Size(85, 35);
            unlockButton.TabIndex = 0;
            unlockButton.Text = "button1";
            unlockButton.UseVisualStyleBackColor = true;
            unlockButton.Click += button1_Click;
            // 
            // LockedLabel
            // 
            LockedLabel.Font = new System.Drawing.Font("Georgia", 48F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            LockedLabel.Location = new System.Drawing.Point(3, 3);
            LockedLabel.Name = "LockedLabel";
            LockedLabel.Size = new System.Drawing.Size(594, 75);
            LockedLabel.TabIndex = 1;
            LockedLabel.Text = "label1";
            LockedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            LockedLabel.Paint += LockedLabel_Paint;
            // 
            // pincode
            // 
            pincode.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            pincode.Location = new System.Drawing.Point(123, 78);
            pincode.Name = "pincode";
            pincode.Size = new System.Drawing.Size(223, 35);
            pincode.TabIndex = 2;
            pincode.UseSystemPasswordChar = true;
            pincode.TextChanged += pincode_TextChanged;
            pincode.KeyPress += pincode_KeyPress;
            // 
            // panel1
            // 
            panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panel1.Controls.Add(ErrorLabel);
            panel1.Controls.Add(LockedLabel);
            panel1.Controls.Add(pincode);
            panel1.Controls.Add(unlockButton);
            panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            panel1.Location = new System.Drawing.Point(104, 58);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(600, 177);
            panel1.TabIndex = 3;
            panel1.Paint += panel1_Paint;
            // 
            // ErrorLabel
            // 
            ErrorLabel.AutoSize = true;
            ErrorLabel.Font = new System.Drawing.Font("Georgia", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            ErrorLabel.ForeColor = System.Drawing.Color.Red;
            ErrorLabel.Location = new System.Drawing.Point(260, 127);
            ErrorLabel.Name = "ErrorLabel";
            ErrorLabel.Size = new System.Drawing.Size(75, 29);
            ErrorLabel.TabIndex = 3;
            ErrorLabel.Text = "label1";
            // 
            // ScreenLockerForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(panel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            KeyPreview = true;
            MinimizeBox = false;
            Name = "ScreenLockerForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "ScreenLockerForm";
            TopMost = true;
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Deactivate += ScreenLockerForm_Deactivate;
            FormClosing += ScreenLockerForm_FormClosing;
            Paint += ScreenLockerForm_Paint;
            KeyDown += ScreenLockerForm_KeyDown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button unlockButton;
        private System.Windows.Forms.Label LockedLabel;
        private System.Windows.Forms.TextBox pincode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ErrorLabel;
    }
}