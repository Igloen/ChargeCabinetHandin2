using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeCabinetLibrary
{
   public class ConsoleWriter : IConsoleWriter
    {

        public void DoorOpened()
        {
            Console.WriteLine("Tilslut telefon");
        }

        public void DoorClosed()
        {
            Console.WriteLine("Indlæs RFID");
        }

        public void WrongRFid()
        {
            Console.WriteLine("Forkert RFID tag");
        }

        public void UnlockedMessage()
        {
            Console.WriteLine("Tag din telefon ud af skabet og luk døren");
        }

        public void LockedMessage()
        {
            Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
        }

        public void NotConnectedMessage()
        {
            Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
        }

        public void CloseDoorMessage()
        {
            Console.WriteLine("Døren er ikke lukket og eller din mobil er ikke sat ordenligt i laderen");
        }
    }
}
