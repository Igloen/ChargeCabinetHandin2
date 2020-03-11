using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeCabinetLibrary
{
    public class RFidReader
    {
        private int id;

        public event EventHandler<RFidChangedEventArgs> RFidChangedEvent;

        public void SetID(int newID)
        {
            OnRFidChanged(new RFidChangedEventArgs { ID = newID});
        }


        protected virtual void OnRFidChanged(RFidChangedEventArgs e)
        {
            RFidChangedEvent?.Invoke(this, e);
        }

    }



    public class RFidChangedEventArgs : EventArgs
    {
        public int ID { get; set; }
    }
}

