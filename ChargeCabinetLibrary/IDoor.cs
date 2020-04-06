using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChargeCabinetLibrary
{

    public class DoorStateChangedEventArgs : EventArgs
    {
        public bool StateOpen { get; set; }

    }

    public class LockStateChangedEventArgs : EventArgs
    {
        public bool StateLocked { get; set; }

    }


    public interface IDoor
    {
        
        event EventHandler<DoorStateChangedEventArgs> DoorChangedEvent;
        event EventHandler<LockStateChangedEventArgs> LockChangedEvent;

        void SetDoorState(bool state);

        void LockDoor(); 

        void UnlockDoor(); 

        void OnDoorStateChanged(DoorStateChangedEventArgs e);

    }
}
