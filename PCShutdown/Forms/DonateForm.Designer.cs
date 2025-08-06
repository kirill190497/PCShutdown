namespace PCShutdown.Forms
{
    partial class DonateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DonateForm));
            donation_wallet = new System.Windows.Forms.ComboBox();
            sendLabel = new System.Windows.Forms.Label();
            qr_image = new System.Windows.Forms.PictureBox();
            label1 = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            button1 = new System.Windows.Forms.Button();
            tgLink = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)qr_image).BeginInit();
            SuspendLayout();
            // 
            // donation_wallet
            // 
            donation_wallet.FormattingEnabled = true;
            donation_wallet.Location = new System.Drawing.Point(60, 12);
            donation_wallet.Name = "donation_wallet";
            donation_wallet.Size = new System.Drawing.Size(202, 23);
            donation_wallet.TabIndex = 0;
            donation_wallet.SelectedIndexChanged += donation_wallet_SelectedIndexChanged;
            // 
            // sendLabel
            // 
            sendLabel.AutoSize = true;
            sendLabel.Location = new System.Drawing.Point(12, 15);
            sendLabel.Name = "sendLabel";
            sendLabel.Size = new System.Drawing.Size(33, 15);
            sendLabel.TabIndex = 1;
            sendLabel.Text = "Send";
            // 
            // qr_image
            // 
            qr_image.Location = new System.Drawing.Point(11, 99);
            qr_image.Name = "qr_image";
            qr_image.Size = new System.Drawing.Size(250, 250);
            qr_image.TabIndex = 2;
            qr_image.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 42);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(38, 15);
            label1.TabIndex = 3;
            label1.Text = "label1";
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(12, 355);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(249, 23);
            textBox1.TabIndex = 4;
            textBox1.Click += textBox1_Click;
            textBox1.Enter += textBox1_Enter;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(12, 384);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(249, 23);
            button1.TabIndex = 5;
            button1.Text = "Copy wallet";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // tgLink
            // 
            tgLink.AutoSize = true;
            tgLink.Location = new System.Drawing.Point(75, 68);
            tgLink.Name = "tgLink";
            tgLink.Size = new System.Drawing.Size(81, 15);
            tgLink.TabIndex = 6;
            tgLink.TabStop = true;
            tgLink.Text = "Telegram Link";
            tgLink.LinkClicked += tgLink_LinkClicked;
            // 
            // DonateForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(273, 422);
            Controls.Add(tgLink);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(qr_image);
            Controls.Add(sendLabel);
            Controls.Add(donation_wallet);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DonateForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "PCShutdown - Donate";
            Load += DonateForm_Load;
            ((System.ComponentModel.ISupportInitialize)qr_image).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox donation_wallet;
        private System.Windows.Forms.Label sendLabel;
        private System.Windows.Forms.PictureBox qr_image;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.LinkLabel tgLink;
    }
}