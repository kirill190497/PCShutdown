using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PCShutdown.Classes
{
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public string Question { get; set; }
        public string ShortQuestion { get; set; }

        public ComboBoxItem(string text, string value, string question = "", string short_question = "")
        {
            Text = text;
            Value = value;
            if (question != null && question != "") 
            {
                Question = question;
            }
            else
            {
                Question = "The language will change after restarting the application. Restart the app now?";
            }

            if (short_question != null && short_question != "")
            {
                ShortQuestion = short_question;
            }
            else
            {
                ShortQuestion = "Restart app now?";
            }

        }

        public override string ToString()
        {
            return Text;
        }
    }

    public class DonateComboBoxItem
    {
        public string WalletAddress { get; set; }
        public string Coin { get; set; }
        public string Network { get; set; }



        public DonateComboBoxItem(string coin, string wallet, string network = "")
        {
            Coin = coin;
            WalletAddress = wallet;
            Network = network;
        }

        public override string ToString()
        {
            string net = Network != "" ? $"({Network})" : "";
            return $"{Coin} {net}";
        }
    }

    public class ActionComboBoxItem
    {
        public string TypeValue { get; set; }
        public string TranslatedType {  get; set; }

        public ActionComboBoxItem(string typeValue, string translatedType)
        {
            TypeValue = typeValue;
            TranslatedType = translatedType;
        }

        public override string ToString()
        {
            return TranslatedType;
        }
    }
}
