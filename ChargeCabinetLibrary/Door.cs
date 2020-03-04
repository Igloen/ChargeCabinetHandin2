using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeCabinetLibrary
{
    public class Door : IDoor
    {
        private bool _doorOpen = false;

        public event EventHandler<DoorStateChangedEventArgs> DoorChangedEvent;

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
