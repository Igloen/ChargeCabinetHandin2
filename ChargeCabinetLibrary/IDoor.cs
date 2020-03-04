using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeCabinetLibrary
{
    public interface IDoor
    {
        event EventHandler<DoorStateChangedEventArgs> DoorChangedEvent;
        
    }
}
