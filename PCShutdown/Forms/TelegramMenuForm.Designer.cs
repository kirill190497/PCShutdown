﻿namespace PCShutdown.Forms
{
    partial class TelegramMenuForm
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
            AddRow = new System.Windows.Forms.Button();
            ColsCount = new System.Windows.Forms.NumericUpDown();
            menu_lst = new System.Windows.Forms.GroupBox();
            saveMenu = new System.Windows.Forms.Button();
            loadDefault = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)ColsCount).BeginInit();
            SuspendLayout();
            // 
            // AddRow
            // 
            AddRow.Location = new System.Drawing.Point(266, 12);
            AddRow.Name = "AddRow";
            AddRow.Size = new System.Drawing.Size(127, 23);
            AddRow.TabIndex = 0;
            AddRow.Text = "Add";
            AddRow.UseVisualStyleBackColor = true;
            AddRow.Click += AddRow_Click;
            // 
            // ColsCount
            // 
            ColsCount.Location = new System.Drawing.Point(167, 12);
            ColsCount.Maximum = new decimal(new int[] { 3, 0, 0, 0 });
            ColsCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ColsCount.Name = "ColsCount";
            ColsCount.Size = new System.Drawing.Size(93, 23);
            ColsCount.TabIndex = 1;
            ColsCount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // menu_lst
            // 
            menu_lst.Location = new System.Drawing.Point(12, 41);
            menu_lst.Name = "menu_lst";
            menu_lst.Size = new System.Drawing.Size(381, 239);
            menu_lst.TabIndex = 2;
            menu_lst.TabStop = false;
            menu_lst.Text = "groupBox1";
            // 
            // saveMenu
            // 
            saveMenu.Location = new System.Drawing.Point(318, 478);
            saveMenu.Name = "saveMenu";
            saveMenu.Size = new System.Drawing.Size(75, 23);
            saveMenu.TabIndex = 3;
            saveMenu.Text = "button1";
            saveMenu.UseVisualStyleBackColor = true;
            saveMenu.Click += saveMenu_Click;
            // 
            // loadDefault
            // 
            loadDefault.Location = new System.Drawing.Point(12, 478);
            loadDefault.Name = "loadDefault";
            loadDefault.Size = new System.Drawing.Size(93, 23);
            loadDefault.TabIndex = 4;
            loadDefault.Text = "Default";
            loadDefault.UseVisualStyleBackColor = true;
            loadDefault.Click += loadDefault_Click;
            // 
            // TelegramMenuForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(405, 513);
            Controls.Add(loadDefault);
            Controls.Add(saveMenu);
            Controls.Add(menu_lst);
            Controls.Add(ColsCount);
            Controls.Add(AddRow);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Name = "TelegramMenuForm";
            Text = "TelegramMenuForm";
            Load += TelegramMenuForm_Load;
            ((System.ComponentModel.ISupportInitialize)ColsCount).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button AddRow;
        private System.Windows.Forms.NumericUpDown ColsCount;
        private System.Windows.Forms.GroupBox menu_lst;
        private System.Windows.Forms.Button saveMenu;
        private System.Windows.Forms.Button loadDefault;
    }
}