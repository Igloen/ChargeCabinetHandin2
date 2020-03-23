using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeCabinetLibrary
{
    public interface IChargeControl
    {
        int _state { get;}

        bool IsConnected();

        void StartCharge();

        void StopCharge();

        void HandleCurrentValueEvent(object sender, CurrentEventArgs e);



    }
}
