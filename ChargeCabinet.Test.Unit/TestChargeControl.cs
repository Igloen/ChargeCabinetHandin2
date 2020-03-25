using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChargeCabinetLibrary;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace ChargeCabinet.Test.Unit
{
    [TestFixture]
    public class TestChargeControl
    {
        private IChargeControl _uut;
        private IUsbCharger _usbCharger;
        private CurrentEventArgs _receivedEventArgs;

        [SetUp]
        public void Setup()
        {
            _usbCharger = new UsbChargerSimulator();
            
            _uut = new ChargeControl(_usbCharger);

            _receivedEventArgs = null;

            _usbCharger.CurrentValueEvent += (o, args) =>
            {
                _receivedEventArgs = args;
            };


        }

        [TestCase(6)]
        [TestCase(243)]
        [TestCase(500)]
        public void testCurrentCharge_State2_charging(double CurrentValue)
        {
            _uut.HandleCurrentValueEvent(_usbCharger, new CurrentEventArgs() { Current = CurrentValue });


            Assert.That(_uut._state, Is.EqualTo(2));


        }

        [TestCase(3)]
        [TestCase(2)]
        [TestCase(1)]
        public void testCurrentCharge_State1_FullyCharged(double CurrentValue)
        {

            _uut.HandleCurrentValueEvent(_usbCharger, new CurrentEventArgs() { Current = CurrentValue });

            Assert.That(_uut._state, Is.EqualTo(1));

        }

        [Test]
        public void testCurrentCharge_State3_Overload()
        {
            _usbCharger.SimulateOverload(true);
            _uut.StartCharge();
            Assert.That(_uut._state, Is.EqualTo(3));

        }

        [Test]
        public void testCurrentCharge_State0_NotConnected()
        {

            _usbCharger.SimulateConnected(false);
            _uut.StartCharge();
            Assert.That(_uut._state, Is.EqualTo(0));



        }



        [TestCase(true)]
        [TestCase(false)]
        public void testConnected(bool state)
        {

            _usbCharger.SimulateConnected(state);

            Assert.That(_uut.IsConnected(), Is.EqualTo(state));

        }

        [Test]
        public void TestStartCharge()
        {
            
            _uut.StartCharge();

            Assert.That(_receivedEventArgs,Is.Not.Null);

        }

        [Test]
        public void TestStopCharge()
        {

            _uut.StopCharge();

            Assert.That(_receivedEventArgs, Is.Not.Null);

        }



    }
}
