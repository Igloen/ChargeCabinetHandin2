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
        public ChargeControl()
        {
            _charger = new UsbChargerSimulator(); //??? eller parameter?
        }

        public bool IsConnected()
        {
            bool connection = _charger.Connected;

            return connection;
        }

        public void StartCharge()
        {
            _charger.StartCharge();
        }

        public void StopCharge()
        {

        }


    }
}
