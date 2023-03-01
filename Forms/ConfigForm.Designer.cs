
namespace PCShutdown.Forms
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
            this.DelayLabel = new System.Windows.Forms.Label();
            this.delayValue = new System.Windows.Forms.TextBox();
            this.autorun = new System.Windows.Forms.CheckBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.passwordCheck = new System.Windows.Forms.CheckBox();
            this.showPassword = new System.Windows.Forms.CheckBox();
            this.workPathDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.WorkDirPath = new System.Windows.Forms.TextBox();
            this.browse = new System.Windows.Forms.Button();
            this.checkMac = new System.Windows.Forms.CheckBox();
            this.pin_label = new System.Windows.Forms.Label();
            this.unlock_pin = new System.Windows.Forms.TextBox();
            this.showPin = new System.Windows.Forms.CheckBox();
            this.Language = new System.Windows.Forms.ComboBox();
            this.LangLabel = new System.Windows.Forms.Label();
            this.advancedGroup = new System.Windows.Forms.GroupBox();
            this.tgGroup = new System.Windows.Forms.GroupBox();
            this.TelegramAdmin = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TelegramBotToken = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PortLabel = new System.Windows.Forms.Label();
            this.ServerPort = new System.Windows.Forms.TextBox();
            this.advancedGroup.SuspendLayout();
            this.tgGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(151, 467);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 25);
            this.button1.TabIndex = 0;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DelayLabel
            // 
            this.DelayLabel.AutoSize = true;
            this.DelayLabel.Location = new System.Drawing.Point(15, 15);
            this.DelayLabel.Name = "DelayLabel";
            this.DelayLabel.Size = new System.Drawing.Size(89, 15);
            this.DelayLabel.TabIndex = 1;
            this.DelayLabel.Text = "Задержка (сек)";
            // 
            // delayValue
            // 
            this.delayValue.Location = new System.Drawing.Point(110, 12);
            this.delayValue.Name = "delayValue";
            this.delayValue.PlaceholderText = "Задержка";
            this.delayValue.Size = new System.Drawing.Size(65, 23);
            this.delayValue.TabIndex = 2;
            this.delayValue.TextChanged += new System.EventHandler(this.delayValue_TextChanged);
            this.delayValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.delayValue_KeyPress);
            // 
            // autorun
            // 
            this.autorun.AutoSize = true;
            this.autorun.Location = new System.Drawing.Point(15, 69);
            this.autorun.Name = "autorun";
            this.autorun.Size = new System.Drawing.Size(194, 19);
            this.autorun.TabIndex = 3;
            this.autorun.Text = "Запускать при старте системы";
            this.autorun.UseVisualStyleBackColor = true;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(15, 43);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(49, 15);
            this.PasswordLabel.TabIndex = 4;
            this.PasswordLabel.Text = "Пароль";
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
            this.passwordCheck.Location = new System.Drawing.Point(15, 94);
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
            // WorkDirPath
            // 
            this.WorkDirPath.Location = new System.Drawing.Point(13, 420);
            this.WorkDirPath.Name = "WorkDirPath";
            this.WorkDirPath.ReadOnly = true;
            this.WorkDirPath.Size = new System.Drawing.Size(254, 23);
            this.WorkDirPath.TabIndex = 9;
            this.WorkDirPath.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.WorkDirPath_MouseDoubleClick);
            // 
            // browse
            // 
            this.browse.Location = new System.Drawing.Point(278, 420);
            this.browse.Name = "browse";
            this.browse.Size = new System.Drawing.Size(75, 23);
            this.browse.TabIndex = 10;
            this.browse.Text = "Обзор..";
            this.browse.UseVisualStyleBackColor = true;
            this.browse.Click += new System.EventHandler(this.browse_Click);
            // 
            // checkMac
            // 
            this.checkMac.AutoSize = true;
            this.checkMac.Location = new System.Drawing.Point(15, 119);
            this.checkMac.Name = "checkMac";
            this.checkMac.Size = new System.Drawing.Size(149, 19);
            this.checkMac.TabIndex = 11;
            this.checkMac.Text = "Проверять MAC адрес";
            this.checkMac.UseVisualStyleBackColor = true;
            // 
            // pin_label
            // 
            this.pin_label.AutoSize = true;
            this.pin_label.Enabled = false;
            this.pin_label.Location = new System.Drawing.Point(6, 24);
            this.pin_label.Name = "pin_label";
            this.pin_label.Size = new System.Drawing.Size(54, 15);
            this.pin_label.TabIndex = 12;
            this.pin_label.Text = "Пин-код";
            // 
            // unlock_pin
            // 
            this.unlock_pin.Location = new System.Drawing.Point(101, 21);
            this.unlock_pin.Name = "unlock_pin";
            this.unlock_pin.Size = new System.Drawing.Size(100, 23);
            this.unlock_pin.TabIndex = 13;
            this.unlock_pin.Text = "1904";
            this.unlock_pin.UseSystemPasswordChar = true;
            // 
            // showPin
            // 
            this.showPin.AutoSize = true;
            this.showPin.Enabled = false;
            this.showPin.Location = new System.Drawing.Point(207, 23);
            this.showPin.Name = "showPin";
            this.showPin.Size = new System.Drawing.Size(124, 19);
            this.showPin.TabIndex = 14;
            this.showPin.Text = "Показать пин-код";
            this.showPin.UseVisualStyleBackColor = true;
            this.showPin.CheckedChanged += new System.EventHandler(this.showPin_CheckedChanged);
            // 
            // Language
            // 
            this.Language.FormattingEnabled = true;
            this.Language.Location = new System.Drawing.Point(110, 144);
            this.Language.Name = "Language";
            this.Language.Size = new System.Drawing.Size(242, 23);
            this.Language.TabIndex = 15;
            // 
            // LangLabel
            // 
            this.LangLabel.AutoSize = true;
            this.LangLabel.Location = new System.Drawing.Point(15, 147);
            this.LangLabel.Name = "LangLabel";
            this.LangLabel.Size = new System.Drawing.Size(34, 15);
            this.LangLabel.TabIndex = 16;
            this.LangLabel.Text = "Язык";
            // 
            // advancedGroup
            // 
            this.advancedGroup.Controls.Add(this.tgGroup);
            this.advancedGroup.Controls.Add(this.PortLabel);
            this.advancedGroup.Controls.Add(this.ServerPort);
            this.advancedGroup.Controls.Add(this.pin_label);
            this.advancedGroup.Controls.Add(this.unlock_pin);
            this.advancedGroup.Controls.Add(this.showPin);
            this.advancedGroup.Enabled = false;
            this.advancedGroup.Location = new System.Drawing.Point(15, 173);
            this.advancedGroup.Name = "advancedGroup";
            this.advancedGroup.Size = new System.Drawing.Size(337, 241);
            this.advancedGroup.TabIndex = 17;
            this.advancedGroup.TabStop = false;
            this.advancedGroup.Text = "groupBox1";
            this.advancedGroup.Enter += new System.EventHandler(this.advancedGroup_Enter);
            // 
            // tgGroup
            // 
            this.tgGroup.Controls.Add(this.TelegramAdmin);
            this.tgGroup.Controls.Add(this.label2);
            this.tgGroup.Controls.Add(this.TelegramBotToken);
            this.tgGroup.Controls.Add(this.label1);
            this.tgGroup.Location = new System.Drawing.Point(6, 81);
            this.tgGroup.Name = "tgGroup";
            this.tgGroup.Size = new System.Drawing.Size(325, 83);
            this.tgGroup.TabIndex = 18;
            this.tgGroup.TabStop = false;
            this.tgGroup.Text = "Telegram Bot";
            // 
            // TelegramAdmin
            // 
            this.TelegramAdmin.Location = new System.Drawing.Point(112, 51);
            this.TelegramAdmin.Name = "TelegramAdmin";
            this.TelegramAdmin.Size = new System.Drawing.Size(207, 23);
            this.TelegramAdmin.TabIndex = 20;
            this.TelegramAdmin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TelegramAdmin_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 15);
            this.label2.TabIndex = 19;
            this.label2.Text = "Admin chat ID";
            // 
            // TelegramBotToken
            // 
            this.TelegramBotToken.Location = new System.Drawing.Point(112, 22);
            this.TelegramBotToken.Name = "TelegramBotToken";
            this.TelegramBotToken.Size = new System.Drawing.Size(207, 23);
            this.TelegramBotToken.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 15);
            this.label1.TabIndex = 15;
            this.label1.Text = "Bot Token";
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(6, 55);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(61, 15);
            this.PortLabel.TabIndex = 18;
            this.PortLabel.Text = "ServerPort";
            // 
            // ServerPort
            // 
            this.ServerPort.Location = new System.Drawing.Point(101, 52);
            this.ServerPort.Name = "ServerPort";
            this.ServerPort.Size = new System.Drawing.Size(100, 23);
            this.ServerPort.TabIndex = 17;
            this.ServerPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(364, 501);
            this.Controls.Add(this.advancedGroup);
            this.Controls.Add(this.LangLabel);
            this.Controls.Add(this.Language);
            this.Controls.Add(this.checkMac);
            this.Controls.Add(this.browse);
            this.Controls.Add(this.WorkDirPath);
            this.Controls.Add(this.showPassword);
            this.Controls.Add(this.passwordCheck);
            this.Controls.Add(this.password);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.autorun);
            this.Controls.Add(this.delayValue);
            this.Controls.Add(this.DelayLabel);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ConfigForm";
            this.Text = "ConfigForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigForm_FormClosing);
            this.advancedGroup.ResumeLayout(false);
            this.advancedGroup.PerformLayout();
            this.tgGroup.ResumeLayout(false);
            this.tgGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label DelayLabel;
        private System.Windows.Forms.TextBox delayValue;
        private System.Windows.Forms.CheckBox autorun;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.CheckBox passwordCheck;
        private System.Windows.Forms.CheckBox showPassword;
        private System.Windows.Forms.FolderBrowserDialog workPathDialog;
        private System.Windows.Forms.TextBox WorkDirPath;
        private System.Windows.Forms.Button browse;
        private System.Windows.Forms.CheckBox checkMac;
        private System.Windows.Forms.Label pin_label;
        private System.Windows.Forms.TextBox unlock_pin;
        private System.Windows.Forms.CheckBox showPin;
        private System.Windows.Forms.ComboBox Language;
        private System.Windows.Forms.Label LangLabel;
        private System.Windows.Forms.GroupBox advancedGroup;
        private System.Windows.Forms.TextBox TelegramBotToken;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.TextBox ServerPort;
        private System.Windows.Forms.GroupBox tgGroup;
        private System.Windows.Forms.TextBox TelegramAdmin;
        private System.Windows.Forms.Label label2;
    }
}