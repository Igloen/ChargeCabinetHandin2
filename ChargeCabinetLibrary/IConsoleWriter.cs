using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeCabinetLibrary
{
    public interface IConsoleWriter
    {

        void DoorOpened();


        void DoorClosed();

        void WrongRFid();

        void UnlockedMessage();

        void LockedMessage();

        void NotConnectedMessage();

        void CloseDoorMessage();
    }
}
