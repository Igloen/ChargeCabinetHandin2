using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChargeCabinetLibrary
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen 
        }

        // Her mangler flere member variable
        private LadeskabState _state;
        //private IUsbCharger _charger;
        private ChargeControl _chargeControl;
        private IRFidReader _reader; 
        private int _oldId;
        private IDoor _door;
        private DoorStateChangedEventArgs _doorState;

        private string logFile = "logfile.txt";                             // Navnet på systemets log-fil

        public bool _doorOpen { get; set;} 


        public StationControl(IDoor door, IRFidReader reader, ChargeControl chargeControl)
        {
            _door = door; 
            _reader = reader;
            _chargeControl = chargeControl;


            _door.DoorChangedEvent += HandleDoorChangedEvent;               //Sørger for at når der sker et event i døren, så kaldes event-handleren
            _reader.RFidChangedEvent += HandleRFidReaderchangedEvent;
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_chargeControl.IsConnected())
                    {
                        _door.LockDoor();
                        _chargeControl.StartCharge();
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:

                 //intet sker her 

                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _chargeControl.StopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
                    }

                    break;
            }
        }

        public void DoorOpened()
        {
            Console.WriteLine("Tilslut telefon");
        }


        public void DoorClosed()
        {
            Console.WriteLine("Indlæs RFID");
        }


        private void HandleDoorChangedEvent(object sender, DoorStateChangedEventArgs e)
        {
            if (e.StateOpen == true)                     //Når en person åbner skabet
            {
                DoorOpened();
                _state = LadeskabState.DoorOpen;
            }

            if (e.StateOpen == false)                   // Når en person lukker skabet
            {
                DoorClosed();
                _state = LadeskabState.Available;
            }

        }

        private void HandleRFidReaderchangedEvent(object sender, RFidChangedEventArgs e)
        {
            RfidDetected(e.ID);
        }


        // Her mangler de andre trigger handlere
    }
}

