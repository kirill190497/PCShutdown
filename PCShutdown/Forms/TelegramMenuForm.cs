using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;
using PCShutdown.Classes;
using PCShutdown.Classes.DarkMode;
using PCShutdown.Classes.TelegramBot;

namespace PCShutdown.Forms
{
    public partial class TelegramMenuForm : Form
    {
        private readonly Strings S = ShutdownApp.Translation.Lang.Strings;
        private int Rows = 0;

        private List<object> Actions;
        private string DeleteItemString;

        public TelegramMenuForm()
        {

            InitializeComponent();
            Actions = Enum.GetValues(typeof(ShutdownTask.TaskType)).Cast<object>().ToList();
            DeleteItemString = $"->{S.Delete}<-";
            foreach (var action in Actions.ToList())
            {
                if (!ShutdownTelegramBot.SupportedCommands.Contains(action.ToString()))
                {
                    Actions.Remove(action);
                }
            }
            Actions.Remove(Actions.Find((x) => x.ToString() == "None"));
            Actions.Add(DeleteItemString);
            ApplyTranslation();
            LoadMenu();

        }

        private void CreateComboBox(int row, int col, string selected)
        {
            ComboBox item;
            if (ShutdownApp.Cfg.DarkMode)
            {
                item = new FlatComboBox();
            }
            else
            {
                item = new ComboBox();
            }
            foreach (var action in Actions)
            {
                item.Items.Add(new ActionComboBoxItem(action.ToString(), ShutdownTask.GetTranslatedTypeName(action.ToString())));
            }
            item.SelectedItem = item.Items.Cast<ActionComboBoxItem>().ToList().Find((x) => x.TypeValue == selected);

            item.Name = "it-" + row + "_" + col;
            item.SelectedIndexChanged += ItemSelectedChanged;
            item.Location = new Point((item.Size.Width + 5) * col + 5, (item.Size.Height + 5) * row + 25);
            menu_lst.Controls.Add(item);
        }

        private void CreateAddButton(int row, int col)
        {
            var item = new Button();

            item.Width = 120;

            item.Text = "+";
            item.Location = new Point((item.Size.Width + 5) * col - 1 + 6, (item.Size.Height + 5) * row + 25);
            item.Click += addIt_Click;
            item.Name = "add-" + row + "_" + col;
     
            menu_lst.Controls.Add(item);
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
                    CreateComboBox(j, i, menu[j][i]);
                }
                if (menu[j].Count < 3)
                {
                    for (var i = menu[j].Count + 1; i < 4; i++)
                    {
                        CreateAddButton(j, i-1);
                    }
                }
            }
            
            Rows = menu.Count;

        }

        private void ApplyTranslation()
        {
            Text = S.Settings + " - Telegram " + S.EditTelegramMenu;
            saveMenu.Text = S.SaveButton;
            menu_lst.Text = S.EditTelegramMenu;


        }

        private void TelegramMenuForm_Load(object sender, EventArgs e)
        {

        }

        private void AddFilledRow(params string[] items)
        {
            if (items.Length > 0 && items.Length <= 3 )
            {
                for (var i = 0; i < items.Length;i++)
                { 
                    CreateComboBox(Rows, i, items[i]);
                }
                if (items.Length < 3)
                {
                    for (var i = items.Length+1; i < 4; i++)
                    {

                        CreateAddButton(Rows, i-1);
                    }
                }
                
                Rows++;
            }
            
        }

        private string GetActionItem(int index)
        {
            return Actions[index].ToString();
        }
       

        private void AddRow_Click(object sender, EventArgs e)
        {
            if (Rows < 5)
            {
                int cols = (int)ColsCount.Value;


                for (int i = 0; i < cols; i++)
                {
                   
                   CreateComboBox(Rows, i, GetActionItem(0));
                }
                if (cols < 3)
                {
                    for (var i = cols + 1; i < 4; i++)
                    {

                        CreateAddButton(Rows, i - 1);
                    }
                }

                Rows++;

            }

        }        

        private void addIt_Click(object sender, EventArgs e)
        { 
            Button sndr = sender as Button;
            
            var coords = sndr.Name.Split("-")[^1].Split("_");
            int row = int.Parse(coords[0]);
            int col = int.Parse(coords[1]);
            menu_lst.Controls.Remove(sndr);
            CreateComboBox(row, col, GetActionItem(0));
        }

        private void ItemSelectedChanged(object sender, EventArgs e)
        {
            var s = (ComboBox)sender;
            string[] coords = s.Name.Split("-")[^1].Split("_");
            int row = int.Parse(coords[0]);
            int col = int.Parse(coords[1]);
            if (s.Items[s.SelectedIndex].ToString() == DeleteItemString)
            {
                menu_lst.Controls.Remove(s);
                CreateAddButton(row,col);
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
                    var r = menu_lst.Controls.Find("it-" + i + "_" + j, false).FirstOrDefault() as ComboBox;
                    string item_name = "";
                    if (r != null)
                    {
                        if (r.SelectedItem != null)
                        {
                            var item = (ActionComboBoxItem)r.SelectedItem;
                            item_name = item.TypeValue;
                            row.Add(item_name);
                        }
                        
                       
                    }
                    
                        
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
            AddFilledRow("MediaPause", "VolumeMute", "Screenshot");
            AddFilledRow("Lock", "Unlock");
            AddFilledRow("ShutdownPC", "RebootPC", "Sleep");
        }
    }
}
