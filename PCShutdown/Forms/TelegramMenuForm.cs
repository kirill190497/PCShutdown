using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlueMystic;
using Newtonsoft.Json;
using PCShutdown.Classes;

namespace PCShutdown.Forms
{
    public partial class TelegramMenuForm : Form
    {
        private readonly Strings S = ShutdownApp.Translation.Lang.Strings;
        private int Rows = 0;

        private List<object> Actions = new List<object>
                {
                    "Screenshot",
                    "Lock",
                    "Unlock",
                    "Sleep",
                    "Reboot",
                    "Shutdown",
                    "Cancel",
                    "Pause",
                    "Mute"
                };
        public TelegramMenuForm()
        {

            InitializeComponent();
            if (ShutdownApp.Cfg.DarkMode)
                _ = new DarkModeCS(this);
            ApplyTranslation();
            LoadMenu();

        }

        private void LoadMenu()
        {
            List<List<string>> menu;
            if (ShutdownApp.Cfg.TelegramMenu == null)
            {
                menu = new List<List<string>>();
            }
            else
                menu = ShutdownApp.Cfg.TelegramMenu;
            for (var j = 0; j < menu.Count; j++)
            {
                for (var i = 0; i < menu[j].Count; i++)
                {
                    var item = new ComboBox();
                    item.Items.AddRange(Actions.ToArray());
                    item.SelectedText = menu[j][i];

                    item.Name = "it" + j + "_" + i;

                    item.Location = new Point((item.Size.Width + 5) * i + 5, (item.Size.Height + 5) * j + 25);
                    menu_lst.Controls.Add(item);
                }
            }
        }

        private void ApplyTranslation()
        {
            Text = S.Settings + " - Telegram " + S.EditTelegramMenu;
            saveMenu.Text = S.SaveButton;


        }

        private void TelegramMenuForm_Load(object sender, EventArgs e)
        {

        }

        private void AddFilledRow(params string[] items)
        {
            if (items.Length > 0 && items.Length <= 3 )
            {
                for (var i = 0;i < items.Length;i++)
                {
                    var item = new ComboBox();
                    item.Items.AddRange(Actions.ToArray());
                    item.SelectedItem = items[i];

                    item.Name = "it" + Rows + "_" + i;

                    item.Location = new Point((item.Size.Width + 5) * i + 5, (item.Size.Height + 5) * Rows + 25);
                    menu_lst.Controls.Add(item);
                }
                Rows++;
            }
        }

        private void AddRow_Click(object sender, EventArgs e)
        {
            if (Rows < 5)
            {
                int cols = (int)ColsCount.Value;


                for (int i = 0; i < cols; i++)
                {
                    var item = new ComboBox();
                    item.Items.AddRange(Actions.ToArray());
                    item.SelectedIndex = 0;

                    item.Name = "it" + Rows + "_" + i;

                    item.Location = new Point((item.Size.Width + 5) * i + 5, (item.Size.Height + 5) * Rows + 25);
                    menu_lst.Controls.Add(item);
                }
                Rows++;

            }

        }

        private void saveMenu_Click(object sender, EventArgs e)
        {
            List<List<string>> rows = new();
         

            for (int i = 0; i < Rows; i++)
            {
                List<string> row = new List<string>();
                for (int j = 0; j < 3; j++)
                {
                    var r = menu_lst.Controls.Find("it" + i + "_" + j, false).FirstOrDefault() as ComboBox;
                    if (r != null)
                        row.Add(r.SelectedItem.ToString());
                }
                rows.Add(row);

            }


            ShutdownApp.Cfg.TelegramMenu = rows;

            ShutdownApp.Cfg.Save();

            Hide();
        }

        private void loadDefault_Click(object sender, EventArgs e)
        {
            menu_lst.Controls.Clear();
            Rows = 0;
            AddFilledRow("Pause", "Mute", "Screenshot");
            AddFilledRow("Lock", "Unlock");
            AddFilledRow("Shutdown", "Reboot", "Sleep");
        }
    }
}
