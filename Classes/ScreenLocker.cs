using PCShutdown.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCShutdown.Classes
{
    public class LockPin
    {
        private int[] Pin { get; set; }
        private int Length { get; set; }
        public LockPin(string pin)
        {
            string pinstr = pin;
            try 
            { 
                int.Parse(pinstr);
            }
            catch 
            {
                pinstr = ShutdownApp.Cfg.UnlockPin.ToString();
            }
            Length = pinstr.Length;
            Pin = new int[Length];
            for (int i = 0; i < Length; i++)
            {
                
                Pin[i] = int.Parse(pinstr[i].ToString());
            }

        }

        public bool ComparePin(string pin)
        {
            bool result = true;
            if (pin.Length != Length) result = false;
            else
            {
                for (int i = 0; i < Length; i++)
                {
                    if (Pin[i] != int.Parse(pin[i].ToString()))
                    {
                       result = false;
                    }
                }
            }
            return result;
            
        }
    }

    public class ScreenLocker
    {
        private LockPin PinCode { get; set; }

        public ScreenLocker(string pin) {
            PinCode = new LockPin(pin);
            

        }



        public bool TryUnlock(string pin) 
        {
            bool unlocked = PinCode.ComparePin(pin);
           
            return unlocked;


        }


    }
}
