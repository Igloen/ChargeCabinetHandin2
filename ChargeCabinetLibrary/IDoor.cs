using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChargeCabinetLibrary
{
    public interface IDoor
    {
        event EventHandler<DoorStateChangedEventArgs> DoorChangedEvent;

        void LockDoor(); //Vi ved ikke hvilken type det er, gætter på bool. 

        void UnlockDoor(); //Samme her.
    }
}
