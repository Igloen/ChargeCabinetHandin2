using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChargeCabinetLibrary;
using NSubstitute;
using NUnit.Framework;

namespace ChargeCabinet.Test.Unit
{
    [TestFixture]
    public class TestChargeControl
    {
        private IChargeControl _uut;
        private IUsbCharger _usbCharger;

        [SetUp]
        public void Setup()
        {
            _usbCharger = new UsbChargerSimulator();

            _uut = new ChargeControl(_usbCharger); 
        }

        [TestCase(6)]
        [TestCase(243)]
        [TestCase(500)]
        public void testCurrentCharge_State2_charging(double CurrentValue)
        {



            _uut.HandleCurrentValueEvent(_usbCharger, new CurrentEventArgs() { Current = CurrentValue });

            Assert.That(_uut._state, Is.EqualTo(2));


            // _usbCharger.SimulateConnected(true);
            // _usbCharger.SimulateOverload(false);
            //_usbCharger.StartCharge();

            // Assert.That(_uut._state,Is.EqualTo(2));

        }

        [TestCase(3)]
        [TestCase(2)]
        [TestCase(1)]
        public void testCurrentCharge_State1_FullyCharged(double CurrentValue)
        {
            
            _uut.HandleCurrentValueEvent(_usbCharger,new CurrentEventArgs(){Current = CurrentValue});

            Assert.That(_uut._state, Is.EqualTo(1));

        }



    }
}
