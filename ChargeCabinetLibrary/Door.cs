using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ChargeCabinetLibrary
{
    public class Door : IDoor
    {
        public bool DoorOpen { get; private set; }
        public bool DoorLocked { get; private set; } 

        public event EventHandler<DoorStateChangedEventArgs> DoorChangedEvent;

        public void LockDoor()
        {
            OnDoorStateChanged(new DoorStateChangedEventArgs { StateLocked = true });
            DoorLocked = true;

        }

        public void UnlockDoor()
        {
            OnDoorStateChanged(new DoorStateChangedEventArgs{ StateLocked = false});
            DoorLocked = false;
        }

        public void SetDoorState(bool state)
        {
            if (state != DoorOpen)
            {
                OnDoorStateChanged(new DoorStateChangedEventArgs { StateOpen = state});
                DoorOpen = state;
            }
        }


        public virtual void OnDoorStateChanged(DoorStateChangedEventArgs e)
        {
            DoorChangedEvent?.Invoke(this, e);
        }

    }

    
    

}
