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
        private IDoor _door;
        private IRFidReader _reader;
        private LadeskabState _state;
        private IFileLogger _filelogger;
        private IChargeControl _chargeControl;
        private IConsoleWriter _consoleWriter;


        private int _oldId;

        public bool _doorOpenState { get; private set; }                //Test properties
        public bool _doorLockState { get; private set; }
        public int _stationState { get; private set; }


        public StationControl(IDoor door, 
                                IRFidReader reader, 
                                IChargeControl chargeControl, 
                                IFileLogger fileLogger, 
                                IConsoleWriter consoleWriter)
        {
            _door = door; 
            _reader = reader;
            _chargeControl = chargeControl;
            _filelogger = fileLogger;
            _consoleWriter = consoleWriter;
            

            _door.DoorChangedEvent += HandleDoorChangedEvent;               //Sørger for at når der sker et event i døren, så kaldes event-handleren
            _door.LockChangedEvent += HandlelockChangedEvent;               //Sørger for at når der sker et event i låsen, så kaldes event-handleren
            _reader.RFidChangedEvent += HandleRFidReaderchangedEvent;       //Sørger for at når der sker et event i RFid readeren, så kaldes event-handleren

        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_chargeControl.IsConnected() && _doorOpenState == false)
                    {
                        _door.LockDoor();
                        _chargeControl.StartCharge();
                        _oldId = id;
                        _filelogger.LogDoorLocked(id);

                        _consoleWriter.LockedMessage();             //Skriver at skabet er låst
                        _state = LadeskabState.Locked;
                        _stationState = (int) _state;
                    }
                    else
                    {
                        _consoleWriter.NotConnectedMessage();       // Mobilen sidder ikke ordenligt i lader besked
                    }

                    break;

                case LadeskabState.DoorOpen:

                 _consoleWriter.CloseDoorMessage();
                 

                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _chargeControl.StopCharge();
                        _door.UnlockDoor();
                        _filelogger.LogDoorUnlocked(id);

                        _consoleWriter.UnlockedMessage();           // Døren er oplåst besked
                        _state = LadeskabState.Available;
                        _stationState = (int)_state;
                    }
                    else
                    {
                        _consoleWriter.WrongRFid();                 //Forkert RF-id besked
                    }

                    break;
            }
        }

        
        private void HandleDoorChangedEvent(object sender, DoorStateChangedEventArgs e)
        {
             
            if (e.StateOpen == true && _doorLockState == false)               //Når en person åbner skabet
            {
                _state = LadeskabState.DoorOpen;
                _stationState = (int)_state;

                _doorOpenState = e.StateOpen;
                _consoleWriter.DoorOpened();


            }

            if (e.StateOpen == false && _doorLockState == false)              // Når en person lukker skabet
            {
                _state = LadeskabState.Available;
                _stationState = (int)_state;

                _doorOpenState = e.StateOpen;
                _consoleWriter.DoorClosed();

            }
        }

        private void HandlelockChangedEvent(object sender, LockStateChangedEventArgs e)
        {
            _doorLockState = e.StateLocked;                                        // Når der låses med RFid Reader
        }
        private void HandleRFidReaderchangedEvent(object sender, RFidChangedEventArgs e)
        {
            RfidDetected(e.ID);                                                    // Når RFid bliver registreret
        }


    }
}

