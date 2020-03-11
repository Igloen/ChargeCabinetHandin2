using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeCabinetLibrary
{
    public class Door : IDoor
    {
        public bool _doorOpen = false;
        public bool _doorLocked = false; 

        public event EventHandler<DoorStateChangedEventArgs> DoorChangedEvent;
        public void LockDoor()
        {
            throw new NotImplementedException();
        }

        public void UnlockDoor()
        {
            OnDoorStateChanged(new DoorStateChangedEventArgs{ State = true});
            _doorLocked = false;

        }

        public void SetDoorState(bool state)
        {
            if (state != _doorOpen)
            {
                OnDoorStateChanged(new DoorStateChangedEventArgs { State = state});
                _doorOpen = state;
            }
        }


        protected virtual void OnDoorStateChanged(DoorStateChangedEventArgs e)
        {
            DoorChangedEvent?.Invoke(this, e);
        }

    }

    

    public class DoorStateChangedEventArgs : EventArgs
    {
        public bool State { get; set; }
    }

    

}
