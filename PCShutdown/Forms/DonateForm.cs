using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PCShutdown.Classes;
using QRCoder;

namespace PCShutdown.Forms
{
    public partial class DonateForm : Form
    {
        public DonateForm()
        {
            InitializeComponent();

            donation_wallet.Items.Add(new DonateComboBoxItem("USDT", "TME2mUgd12Mu9aNuK9z8n7qTUU9JyP6vCm", "TRC20"));
            donation_wallet.Items.Add(new DonateComboBoxItem("USDT", "UQBsFHMuFY_uYX8n1cSLYgfdcATJYyG3dIx6phbBZrpxh9Oc", "TON Network"));
            donation_wallet.Items.Add(new DonateComboBoxItem("Bitcoin", "1MrSKwS4kVMorNZiVT94DHgdJEEMhpESxB"));
            donation_wallet.Items.Add(new DonateComboBoxItem("Toncoin", "UQBsFHMuFY_uYX8n1cSLYgfdcATJYyG3dIx6phbBZrpxh9Oc", "TON Network"));
            donation_wallet.Items.Add(new DonateComboBoxItem("Notcoin NOT", "UQBsFHMuFY_uYX8n1cSLYgfdcATJYyG3dIx6phbBZrpxh9Oc", "TON Network"));
            donation_wallet.SelectedIndex = 0;
        }

        private void donation_wallet_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (DonateComboBoxItem)(sender as ComboBox).SelectedItem;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(item.WalletAddress, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            label1.Text = $"Send only {item} on this wallet!";
            qr_image.Image = qrCodeImage;
            qr_image.SizeMode = PictureBoxSizeMode.Zoom;
            textBox1.Text = item.WalletAddress;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
            ShutdownApp.ShowToast("Address copied!");
        }

        private void tgLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://t.me/nikulin_kirill",
                UseShellExecute = true
            });
        }

        private void DonateForm_Load(object sender, EventArgs e)
        {

        }
    }
}
