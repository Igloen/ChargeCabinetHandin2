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

        public event EventHandler<DoorStateChangedEventArgs> DoorChangedEvent;
        public event EventHandler<LockStateChangedEventArgs> LockChangedEvent;


        public void LockDoor()
        {
            OnLockStateChanged(new LockStateChangedEventArgs() { StateLocked = true });
            

        }

        public void UnlockDoor()
        {
            OnLockStateChanged(new LockStateChangedEventArgs(){ StateLocked = false});
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

        public virtual void OnLockStateChanged(LockStateChangedEventArgs e)
        {
            LockChangedEvent?.Invoke(this, e);
        }

    }

    
    

}
