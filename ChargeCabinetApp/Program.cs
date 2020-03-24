﻿using System;
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
            IRFidReader _rfidReader = new RFidReader();
            IDoor _door = new Door();
            IUsbCharger _charger = new UsbChargerSimulator();
            
            ChargeControl _chargeControl = new ChargeControl(_charger);
            StationControl _stationControl = new StationControl(_door, _rfidReader, _chargeControl);


            //Use this

            Console.WriteLine("E: Finish");
            Console.WriteLine("O: Open door");
            Console.WriteLine("C: Close door");
            Console.WriteLine("R: RFID");
            Console.WriteLine("K: Simulate connection");
            Console.WriteLine("L: Simulate overload");
            Console.WriteLine("_____________________________");

            bool finish = false;
            do
            {
                string input;
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
