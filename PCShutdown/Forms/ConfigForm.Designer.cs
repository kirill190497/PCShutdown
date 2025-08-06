
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
            button1 = new System.Windows.Forms.Button();
            DelayLabel = new System.Windows.Forms.Label();
            delayValue = new System.Windows.Forms.TextBox();
            autorun = new System.Windows.Forms.CheckBox();
            PasswordLabel = new System.Windows.Forms.Label();
            password = new System.Windows.Forms.TextBox();
            passwordCheck = new System.Windows.Forms.CheckBox();
            showPassword = new System.Windows.Forms.CheckBox();
            workPathDialog = new System.Windows.Forms.FolderBrowserDialog();
            WorkDirPath = new System.Windows.Forms.TextBox();
            browse = new System.Windows.Forms.Button();
            checkMac = new System.Windows.Forms.CheckBox();
            pin_label = new System.Windows.Forms.Label();
            unlock_pin = new System.Windows.Forms.TextBox();
            showPin = new System.Windows.Forms.CheckBox();
            Language = new System.Windows.Forms.ComboBox();
            LangLabel = new System.Windows.Forms.Label();
            advancedGroup = new System.Windows.Forms.GroupBox();
            tgGroup = new System.Windows.Forms.GroupBox();
            EditTelegramMenu = new System.Windows.Forms.Button();
            TelegramAdmin = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            TelegramBotToken = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            PortLabel = new System.Windows.Forms.Label();
            ServerPort = new System.Windows.Forms.TextBox();
            darkTheme = new System.Windows.Forms.CheckBox();
            YandexAliceGroup = new System.Windows.Forms.GroupBox();
            panel2 = new System.Windows.Forms.Panel();
            kuzya_info = new System.Windows.Forms.Label();
            kuzya_settings = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            advancedGroup.SuspendLayout();
            tgGroup.SuspendLayout();
            YandexAliceGroup.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(149, 612);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(80, 25);
            button1.TabIndex = 0;
            button1.Text = "Сохранить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // DelayLabel
            // 
            DelayLabel.AutoSize = true;
            DelayLabel.Location = new System.Drawing.Point(15, 15);
            DelayLabel.Name = "DelayLabel";
            DelayLabel.Size = new System.Drawing.Size(89, 15);
            DelayLabel.TabIndex = 1;
            DelayLabel.Text = "Задержка (сек)";
            // 
            // delayValue
            // 
            delayValue.Location = new System.Drawing.Point(110, 12);
            delayValue.Name = "delayValue";
            delayValue.PlaceholderText = "Задержка";
            delayValue.Size = new System.Drawing.Size(65, 23);
            delayValue.TabIndex = 2;
            delayValue.TextChanged += delayValue_TextChanged;
            delayValue.KeyPress += delayValue_KeyPress;
            // 
            // autorun
            // 
            autorun.AutoSize = true;
            autorun.Location = new System.Drawing.Point(15, 69);
            autorun.Name = "autorun";
            autorun.Size = new System.Drawing.Size(194, 19);
            autorun.TabIndex = 3;
            autorun.Text = "Запускать при старте системы";
            autorun.UseVisualStyleBackColor = true;
            // 
            // PasswordLabel
            // 
            PasswordLabel.AutoSize = true;
            PasswordLabel.Location = new System.Drawing.Point(15, 43);
            PasswordLabel.Name = "PasswordLabel";
            PasswordLabel.Size = new System.Drawing.Size(49, 15);
            PasswordLabel.TabIndex = 4;
            PasswordLabel.Text = "Пароль";
            // 
            // password
            // 
            password.Location = new System.Drawing.Point(110, 40);
            password.MaxLength = 16;
            password.Name = "password";
            password.PlaceholderText = "Пароль";
            password.Size = new System.Drawing.Size(100, 23);
            password.TabIndex = 5;
            password.UseSystemPasswordChar = true;
            // 
            // passwordCheck
            // 
            passwordCheck.AutoSize = true;
            passwordCheck.Location = new System.Drawing.Point(15, 94);
            passwordCheck.Name = "passwordCheck";
            passwordCheck.Size = new System.Drawing.Size(156, 19);
            passwordCheck.TabIndex = 6;
            passwordCheck.Text = "Подтверждать паролем";
            passwordCheck.UseVisualStyleBackColor = true;
            // 
            // showPassword
            // 
            showPassword.AutoSize = true;
            showPassword.Location = new System.Drawing.Point(216, 43);
            showPassword.Name = "showPassword";
            showPassword.Size = new System.Drawing.Size(119, 19);
            showPassword.TabIndex = 7;
            showPassword.Text = "Показать пароль";
            showPassword.UseVisualStyleBackColor = true;
            showPassword.CheckedChanged += showPassword_CheckedChanged;
            // 
            // WorkDirPath
            // 
            WorkDirPath.Location = new System.Drawing.Point(11, 574);
            WorkDirPath.Name = "WorkDirPath";
            WorkDirPath.ReadOnly = true;
            WorkDirPath.Size = new System.Drawing.Size(324, 23);
            WorkDirPath.TabIndex = 9;
            WorkDirPath.MouseDoubleClick += WorkDirPath_MouseDoubleClick;
            // 
            // browse
            // 
            browse.Location = new System.Drawing.Point(341, 574);
            browse.Name = "browse";
            browse.Size = new System.Drawing.Size(75, 23);
            browse.TabIndex = 10;
            browse.Text = "Обзор..";
            browse.UseVisualStyleBackColor = true;
            browse.Click += browse_Click;
            // 
            // checkMac
            // 
            checkMac.AutoSize = true;
            checkMac.Location = new System.Drawing.Point(15, 119);
            checkMac.Name = "checkMac";
            checkMac.Size = new System.Drawing.Size(149, 19);
            checkMac.TabIndex = 11;
            checkMac.Text = "Проверять MAC адрес";
            checkMac.UseVisualStyleBackColor = true;
            // 
            // pin_label
            // 
            pin_label.AutoSize = true;
            pin_label.Location = new System.Drawing.Point(6, 24);
            pin_label.Name = "pin_label";
            pin_label.Size = new System.Drawing.Size(54, 15);
            pin_label.TabIndex = 12;
            pin_label.Text = "Пин-код";
            // 
            // unlock_pin
            // 
            unlock_pin.Location = new System.Drawing.Point(101, 21);
            unlock_pin.Name = "unlock_pin";
            unlock_pin.Size = new System.Drawing.Size(167, 23);
            unlock_pin.TabIndex = 13;
            unlock_pin.Text = "1904";
            unlock_pin.UseSystemPasswordChar = true;
            // 
            // showPin
            // 
            showPin.AutoSize = true;
            showPin.Location = new System.Drawing.Point(274, 23);
            showPin.Name = "showPin";
            showPin.Size = new System.Drawing.Size(124, 19);
            showPin.TabIndex = 14;
            showPin.Text = "Показать пин-код";
            showPin.UseVisualStyleBackColor = true;
            showPin.CheckedChanged += showPin_CheckedChanged;
            // 
            // Language
            // 
            Language.FormattingEnabled = true;
            Language.Location = new System.Drawing.Point(149, 173);
            Language.Name = "Language";
            Language.Size = new System.Drawing.Size(267, 23);
            Language.TabIndex = 15;
            // 
            // LangLabel
            // 
            LangLabel.AutoSize = true;
            LangLabel.Location = new System.Drawing.Point(15, 176);
            LangLabel.Name = "LangLabel";
            LangLabel.Size = new System.Drawing.Size(34, 15);
            LangLabel.TabIndex = 16;
            LangLabel.Text = "Язык";
            // 
            // advancedGroup
            // 
            advancedGroup.Controls.Add(tgGroup);
            advancedGroup.Controls.Add(PortLabel);
            advancedGroup.Controls.Add(ServerPort);
            advancedGroup.Controls.Add(pin_label);
            advancedGroup.Controls.Add(unlock_pin);
            advancedGroup.Controls.Add(showPin);
            advancedGroup.Enabled = false;
            advancedGroup.Location = new System.Drawing.Point(12, 202);
            advancedGroup.Name = "advancedGroup";
            advancedGroup.Size = new System.Drawing.Size(404, 206);
            advancedGroup.TabIndex = 17;
            advancedGroup.TabStop = false;
            advancedGroup.Text = "groupBox1";
            advancedGroup.Enter += advancedGroup_Enter;
            // 
            // tgGroup
            // 
            tgGroup.Controls.Add(EditTelegramMenu);
            tgGroup.Controls.Add(TelegramAdmin);
            tgGroup.Controls.Add(label2);
            tgGroup.Controls.Add(TelegramBotToken);
            tgGroup.Controls.Add(label1);
            tgGroup.Location = new System.Drawing.Point(6, 81);
            tgGroup.Name = "tgGroup";
            tgGroup.Size = new System.Drawing.Size(392, 113);
            tgGroup.TabIndex = 18;
            tgGroup.TabStop = false;
            tgGroup.Text = "Telegram Bot";
            // 
            // EditTelegramMenu
            // 
            EditTelegramMenu.Location = new System.Drawing.Point(205, 80);
            EditTelegramMenu.Name = "EditTelegramMenu";
            EditTelegramMenu.Size = new System.Drawing.Size(181, 23);
            EditTelegramMenu.TabIndex = 21;
            EditTelegramMenu.Text = "button2";
            EditTelegramMenu.UseVisualStyleBackColor = true;
            EditTelegramMenu.Click += EditTelegramMenu_Click;
            // 
            // TelegramAdmin
            // 
            TelegramAdmin.Location = new System.Drawing.Point(112, 51);
            TelegramAdmin.Name = "TelegramAdmin";
            TelegramAdmin.Size = new System.Drawing.Size(274, 23);
            TelegramAdmin.TabIndex = 20;
            TelegramAdmin.KeyPress += TelegramAdmin_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(9, 54);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(83, 15);
            label2.TabIndex = 19;
            label2.Text = "Admin chat ID";
            // 
            // TelegramBotToken
            // 
            TelegramBotToken.Location = new System.Drawing.Point(112, 22);
            TelegramBotToken.Name = "TelegramBotToken";
            TelegramBotToken.Size = new System.Drawing.Size(274, 23);
            TelegramBotToken.TabIndex = 16;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(9, 25);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(60, 15);
            label1.TabIndex = 15;
            label1.Text = "Bot Token";
            // 
            // PortLabel
            // 
            PortLabel.AutoSize = true;
            PortLabel.Location = new System.Drawing.Point(6, 55);
            PortLabel.Name = "PortLabel";
            PortLabel.Size = new System.Drawing.Size(61, 15);
            PortLabel.TabIndex = 18;
            PortLabel.Text = "ServerPort";
            // 
            // ServerPort
            // 
            ServerPort.Location = new System.Drawing.Point(101, 52);
            ServerPort.Name = "ServerPort";
            ServerPort.Size = new System.Drawing.Size(167, 23);
            ServerPort.TabIndex = 17;
            ServerPort.KeyPress += textBox1_KeyPress;
            // 
            // darkTheme
            // 
            darkTheme.AutoSize = true;
            darkTheme.Location = new System.Drawing.Point(15, 144);
            darkTheme.Name = "darkTheme";
            darkTheme.Size = new System.Drawing.Size(96, 19);
            darkTheme.TabIndex = 18;
            darkTheme.Text = "Тёмная тема";
            darkTheme.UseVisualStyleBackColor = true;
            // 
            // YandexAliceGroup
            // 
            YandexAliceGroup.Controls.Add(panel2);
            YandexAliceGroup.Controls.Add(kuzya_settings);
            YandexAliceGroup.Location = new System.Drawing.Point(11, 414);
            YandexAliceGroup.Name = "YandexAliceGroup";
            YandexAliceGroup.Size = new System.Drawing.Size(405, 154);
            YandexAliceGroup.TabIndex = 19;
            YandexAliceGroup.TabStop = false;
            YandexAliceGroup.Text = "groupBox1";
            // 
            // panel2
            // 
            panel2.Controls.Add(kuzya_info);
            panel2.Location = new System.Drawing.Point(7, 22);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(200, 126);
            panel2.TabIndex = 3;
            // 
            // kuzya_info
            // 
            kuzya_info.AutoSize = true;
            kuzya_info.Dock = System.Windows.Forms.DockStyle.Fill;
            kuzya_info.Location = new System.Drawing.Point(0, 0);
            kuzya_info.Name = "kuzya_info";
            kuzya_info.Size = new System.Drawing.Size(38, 15);
            kuzya_info.TabIndex = 1;
            kuzya_info.Text = "label3";
            // 
            // kuzya_settings
            // 
            kuzya_settings.Location = new System.Drawing.Point(212, 125);
            kuzya_settings.Name = "kuzya_settings";
            kuzya_settings.Size = new System.Drawing.Size(187, 23);
            kuzya_settings.TabIndex = 2;
            kuzya_settings.Text = "button2";
            kuzya_settings.UseVisualStyleBackColor = true;
            kuzya_settings.Click += kuzya_settings_Click;
            // 
            // panel1
            // 
            panel1.Location = new System.Drawing.Point(216, 69);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(200, 94);
            panel1.TabIndex = 20;
            // 
            // ConfigForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            ClientSize = new System.Drawing.Size(428, 649);
            Controls.Add(YandexAliceGroup);
            Controls.Add(darkTheme);
            Controls.Add(advancedGroup);
            Controls.Add(LangLabel);
            Controls.Add(Language);
            Controls.Add(checkMac);
            Controls.Add(browse);
            Controls.Add(WorkDirPath);
            Controls.Add(showPassword);
            Controls.Add(passwordCheck);
            Controls.Add(password);
            Controls.Add(PasswordLabel);
            Controls.Add(autorun);
            Controls.Add(delayValue);
            Controls.Add(DelayLabel);
            Controls.Add(button1);
            Controls.Add(panel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            Name = "ConfigForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "ConfigForm";
            FormClosing += ConfigForm_FormClosing;
            Load += ConfigForm_Load;
            advancedGroup.ResumeLayout(false);
            advancedGroup.PerformLayout();
            tgGroup.ResumeLayout(false);
            tgGroup.PerformLayout();
            YandexAliceGroup.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.Button EditTelegramMenu;
        private System.Windows.Forms.CheckBox darkTheme;
        private System.Windows.Forms.GroupBox YandexAliceGroup;
        private System.Windows.Forms.Label kuzya_info;
        private System.Windows.Forms.Button change_inputs;
        private System.Windows.Forms.Button kuzya_settings;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button kuzya_channels;
    }
}