using PCShutdown.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCShutdown.Forms
{
    public partial class KuzyaSettingsForm : Form
    {
        static Strings S = ShutdownApp.Translation.Lang.Strings;
        public KuzyaSettingsForm()
        {
            InitializeComponent();
            SaveKuzya.Text = S.SaveButton;
            tabPage1.Text = S.KuzyaInputs;
            tabPage2.Text = S.KuzyaChannels;

        }
    }
}
