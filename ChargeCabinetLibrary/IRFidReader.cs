using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeCabinetLibrary
{
   public interface IRFidReader
   {
       event EventHandler<RFidChangedEventArgs> RFidChangedEvent;

       void SetID(int newID);

   }
}
