using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeCabinetLibrary
{
    public class ChargeControl
    {
        private IUsbCharger _charger;

        public ChargeControl(IUsbCharger charger)
        {
            _charger = charger;
        }

        public bool IsConnected()
        {
            return _charger.Connected;
        }

        public void StartCharge()
        {
            _charger.StartCharge();
        }

        public void StopCharge()
        {
            _charger.StopCharge();
        }

        private void CurrentchangedEvent(object sender, CurrentEventArgs e)
        {

            if (e.Current == 0)
            {
                //Intet sker
            }

            if (e.Current > 0 && e.Current <= 5)
            {
                Console.WriteLine("Mobil opladning: 100%");
            }

            if (e.Current > 5 && e.Current <= 500)
            {
                Console.WriteLine("Mobil Opladning: Oplader");
            }

            if (e.Current > 500)
            {
                System.Console.WriteLine("Der er noget galt, tag straks mobilen ud af laderen.");
                _charger.StopCharge();
            }


        }


    }
}
