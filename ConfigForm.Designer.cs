
namespace PCShutdown
{
    partial class ConfigForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.delayValue = new System.Windows.Forms.TextBox();
            this.autorun = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.passwordCheck = new System.Windows.Forms.CheckBox();
            this.showPassword = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(150, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 25);
            this.button1.TabIndex = 0;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Задержка (сек)";
            // 
            // delayValue
            // 
            this.delayValue.Location = new System.Drawing.Point(110, 12);
            this.delayValue.Name = "delayValue";
            this.delayValue.PlaceholderText = "Задержка";
            this.delayValue.Size = new System.Drawing.Size(65, 23);
            this.delayValue.TabIndex = 2;
            this.delayValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.delayValue_KeyPress);
            // 
            // autorun
            // 
            this.autorun.AutoSize = true;
            this.autorun.Location = new System.Drawing.Point(15, 74);
            this.autorun.Name = "autorun";
            this.autorun.Size = new System.Drawing.Size(194, 19);
            this.autorun.TabIndex = 3;
            this.autorun.Text = "Запускать при старте системы";
            this.autorun.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Пароль";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(110, 40);
            this.password.MaxLength = 16;
            this.password.Name = "password";
            this.password.PlaceholderText = "Пароль";
            this.password.Size = new System.Drawing.Size(100, 23);
            this.password.TabIndex = 5;
            this.password.UseSystemPasswordChar = true;
            // 
            // passwordCheck
            // 
            this.passwordCheck.AutoSize = true;
            this.passwordCheck.Location = new System.Drawing.Point(15, 99);
            this.passwordCheck.Name = "passwordCheck";
            this.passwordCheck.Size = new System.Drawing.Size(156, 19);
            this.passwordCheck.TabIndex = 6;
            this.passwordCheck.Text = "Подтверждать паролем";
            this.passwordCheck.UseVisualStyleBackColor = true;
            // 
            // showPassword
            // 
            this.showPassword.AutoSize = true;
            this.showPassword.Location = new System.Drawing.Point(216, 43);
            this.showPassword.Name = "showPassword";
            this.showPassword.Size = new System.Drawing.Size(119, 19);
            this.showPassword.TabIndex = 7;
            this.showPassword.Text = "Показать пароль";
            this.showPassword.UseVisualStyleBackColor = true;
            this.showPassword.CheckedChanged += new System.EventHandler(this.showPassword_CheckedChanged);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(364, 261);
            this.Controls.Add(this.showPassword);
            this.Controls.Add(this.passwordCheck);
            this.Controls.Add(this.password);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.autorun);
            this.Controls.Add(this.delayValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ConfigForm";
            this.Text = "ConfigForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox delayValue;
        private System.Windows.Forms.CheckBox autorun;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.CheckBox passwordCheck;
        private System.Windows.Forms.CheckBox showPassword;
    }
}