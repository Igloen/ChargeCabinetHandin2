using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ChargeCabinetLibrary;
using NSubstitute;

namespace ChargeCabinet.Test.Unit
{
    [TestFixture]
    public class TestStationControl
    {
        private StationControl _uut;
        private IDoor _door;
        private IRFidReader _rfidReader;
        private IChargeControl _chargeControl;

        private DoorStateChangedEventArgs _receivedEventArgs;

        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<IDoor>();
            _rfidReader = Substitute.For<IRFidReader>();
            _chargeControl = Substitute.For<IChargeControl>();
            

            _uut = new StationControl(_door, _rfidReader, _chargeControl);

            _receivedEventArgs = null;

            //_uut. += (o, args) =>
            //{
            //    _receivedEventArgs = args;
            //};


        }


        [Test]
        public void HandleDoorChangedEventTest()
        {
         
         _door.SetDoorState(true);
         

            //Assert.That(_receivedEventArgs, Is.Not.Null);

            Assert.That();
        }

        [Test]
        public void DoorOpenTest()
        {
            
        }


    }
}
