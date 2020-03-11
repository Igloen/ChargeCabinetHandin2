using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeCabinetLibrary
{
   public interface IRFidReader
   {
      void SetID(int newID);

      void OnRFidChanged(RFidChangedEventArgs e); // SKal denne her stå her???????????? 

   }
}
