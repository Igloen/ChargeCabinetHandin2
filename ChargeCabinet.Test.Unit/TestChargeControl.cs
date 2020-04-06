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
        private IConsoleWriter _consoleWriter;

        [SetUp]
        public void Setup()
        {
            _usbCharger = Substitute.For<IUsbCharger>();
            _consoleWriter = Substitute.For<IConsoleWriter>();

            _uut = new ChargeControl(_usbCharger, _consoleWriter);


        }

        [TestCase(6)]
        [TestCase(243)]
        [TestCase(500)]
        public void testCurrentCharge_State2_charging(double CurrentValue)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = CurrentValue });

            Assert.That(_uut._state, Is.EqualTo(2));
        }

        [TestCase(3)]
        [TestCase(2)]
        [TestCase(1)]
        public void testCurrentCharge_State1_FullyCharged(double CurrentValue)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() {Current = CurrentValue});

            Assert.That(_uut._state, Is.EqualTo(1));

        }

       
        [TestCase(0)]
        public void testCurrentCharge_State_NothingHappens(double CurrentValue)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = CurrentValue });

            Assert.That(_uut._state, Is.EqualTo(0));
        }


        [TestCase(499, 2)]
        [TestCase(500, 2)]
        [TestCase(501, 3)]
        public void testCurrentCharge_State_Overload(double CurrentValue, double result)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = CurrentValue });

            Assert.That(_uut._state, Is.EqualTo(result));

        }

        [Test]
        public void testCurrentCharge_StartCharge()
        {
            
            _uut.StartCharge();

            _usbCharger.Received(1).StartCharge();
        }

        [Test]
        public void testCurrentCharge_StopCharge()
        {

            _uut.StopCharge();

            _usbCharger.Received(1).StopCharge();


        }


        [TestCase(false)]
        [TestCase(true)]
        public void TestConnection(bool connection)
        {

            _usbCharger.Connected.Returns(connection);

            Assert.That(_uut.IsConnected(), Is.EqualTo(connection));

            

        }


    }
}
