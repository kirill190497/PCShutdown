using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
                    "Shutdown",
                    "Cancel",
                    "Pause",
                    "Mute"
                };
        public TelegramMenuForm()
        {

            InitializeComponent();
            ApplyTranslation();
            LoadMenu();

        }

        private void LoadMenu() 
        {
            var menu = Properties.Settings.Default.TelegramMenu;
            for (var j = 0; j < menu.Count; j++)
            {
                for (var i = 0; i < menu[j].Count; i++)
                {
                    var item = new ComboBox();
                    item.Items.AddRange(Actions.ToArray());
                    item.SelectedText = menu[j][i];
                    
                    item.Name = "it" + j + "_" + i;
                   
                    item.Location = new Point((item.Size.Width + 5) * i + 5, (item.Size.Height  + 5 )* j + 25);
                    menu_lst.Controls.Add(item);
                }
            }
        }

        private void ApplyTranslation()
        {
            this.Text = S.Settings + " - Telegram " + S.EditTelegramMenu;


        }

        private void TelegramMenuForm_Load(object sender, EventArgs e)
        {

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
                   
                    item.Location = new Point((item.Size.Width + 5) * i + 5, (item.Size.Height  + 5 )* Rows + 25);
                    menu_lst.Controls.Add(item);
                }
                Rows++;

            }

        }

        private void saveMenu_Click(object sender, EventArgs e)
        {
            string str = "";
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
            

            Properties.Settings.Default.TelegramMenu = rows;

            Properties.Settings.Default.Save();


        }
    }
}
