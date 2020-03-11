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
            OnDoorStateChanged(new DoorStateChangedEventArgs { StateLocked = true });
        }

        public void UnlockDoor()
        {
            OnDoorStateChanged(new DoorStateChangedEventArgs{ StateLocked = false});
            

        }

        public void SetDoorState(bool state)
        {
            if (state != _doorOpen)
            {
                OnDoorStateChanged(new DoorStateChangedEventArgs { StateOpen = state});
                _doorOpen = state;
            }
        }


        public virtual void OnDoorStateChanged(DoorStateChangedEventArgs e)
        {
            DoorChangedEvent?.Invoke(this, e);
        }

    }

    

    public class DoorStateChangedEventArgs : EventArgs
    {
        public bool StateOpen { get; set; }
        public bool StateLocked { get; set; }
    }

    

}
