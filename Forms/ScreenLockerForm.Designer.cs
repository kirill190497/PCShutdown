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
            this.unlockButton = new System.Windows.Forms.Button();
            this.LockedLabel = new System.Windows.Forms.Label();
            this.pincode = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // unlockButton
            // 
            this.unlockButton.Location = new System.Drawing.Point(352, 78);
            this.unlockButton.Name = "unlockButton";
            this.unlockButton.Size = new System.Drawing.Size(85, 35);
            this.unlockButton.TabIndex = 0;
            this.unlockButton.Text = "button1";
            this.unlockButton.UseVisualStyleBackColor = true;
            this.unlockButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // LockedLabel
            // 
            this.LockedLabel.Font = new System.Drawing.Font("Georgia", 48F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.LockedLabel.Location = new System.Drawing.Point(3, 3);
            this.LockedLabel.Name = "LockedLabel";
            this.LockedLabel.Size = new System.Drawing.Size(594, 75);
            this.LockedLabel.TabIndex = 1;
            this.LockedLabel.Text = "label1";
            this.LockedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LockedLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.LockedLabel_Paint);
            // 
            // pincode
            // 
            this.pincode.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pincode.Location = new System.Drawing.Point(123, 78);
            this.pincode.Name = "pincode";
            this.pincode.Size = new System.Drawing.Size(223, 35);
            this.pincode.TabIndex = 2;
            this.pincode.UseSystemPasswordChar = true;
            this.pincode.TextChanged += new System.EventHandler(this.pincode_TextChanged);
            this.pincode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pincode_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.LockedLabel);
            this.panel1.Controls.Add(this.pincode);
            this.panel1.Controls.Add(this.unlockButton);
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(104, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 127);
            this.panel1.TabIndex = 3;
            // 
            // ScreenLockerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "ScreenLockerForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ScreenLockerForm";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScreenLockerForm_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ScreenLockerForm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ScreenLockerForm_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button unlockButton;
        private System.Windows.Forms.Label LockedLabel;
        private System.Windows.Forms.TextBox pincode;
        private System.Windows.Forms.Panel panel1;
    }
}