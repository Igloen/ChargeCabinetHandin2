using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ChargeCabinetLibrary;

namespace ChargeCabinetApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes
            IDoor _door = new Door();
            IRFidReader _rfidReader = new RFidReader();
            IUsbCharger _charger = new UsbChargerSimulator();

            //Use this

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        //_door.DoorOpened();
                        _door.SetDoorState( true);
                        break;

                    case 'C':
                        //_door.DoorClosed();
                        _door.SetDoorState(false);
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        //_rfidReader.OnRfidRead(id);

                        break;


                    case 'K':
                        System.Console.WriteLine("Telefon er sat til ");
                        _charger.SimulateConnected(true);
                        break;

                    case 'L':
                        System.Console.WriteLine("Kabel Overloaded ");
                        _charger.SimulateOverload(true);
                        break;


                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
