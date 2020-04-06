using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeCabinetLibrary
{
    public class ChargeControl : IChargeControl
    {
        private IUsbCharger _charger;

        private IConsoleWriter _consoleWriter;
        
        public int _state { get; private set; }
        // 0 = intet sker
        // 2 = oplader
        // 1 = Fuldopladt
        // 3 = Overload


        public ChargeControl(IUsbCharger charger, IConsoleWriter consoleWriter)
        {
            _charger = charger;
            _consoleWriter = consoleWriter;

            _charger.CurrentValueEvent += HandleCurrentValueEvent; 
            
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

        public void HandleCurrentValueEvent(object sender, CurrentEventArgs e) //lavet til public eller virker test klassen ikke
        {

            if (e.Current == 0)
            {
                //Intet sker
                _state = 0;
            }

            if (e.Current > 0 && e.Current <= 5)
            {
                _consoleWriter.FullyChargedMessage();
                _state = 1;
            }

            if (e.Current > 5 && e.Current <= 500)
            {
                _consoleWriter.ChargingMessage();
                _state = 2;
            }

            if (e.Current > 500)
            {
                _consoleWriter.OverloadMessage();
                _charger.StopCharge();
                _state = 3;
            }


        }


    }
}
