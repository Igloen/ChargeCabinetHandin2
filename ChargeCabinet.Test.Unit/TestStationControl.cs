using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ChargeCabinetLibrary;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

namespace ChargeCabinet.Test.Unit
{
    [TestFixture]
    public class TestStationControl
    {
        private StationControl _uut;
        private IDoor _door;
        private IRFidReader _rfidReader;
        private IChargeControl _chargeControl;
        private IUsbCharger _usbCharger;

        private DoorStateChangedEventArgs _receivedEventArgsDoor;
        private RFidChangedEventArgs _receivedEventArgsRFid;


        [SetUp]
        public void Setup()
        {
           
           _rfidReader = new RFidReader();
           _door = new Door();
            _usbCharger = Substitute.For<IUsbCharger>();
            _chargeControl = Substitute.For<IChargeControl>();


            _uut = new StationControl(_door, _rfidReader, _chargeControl);

            _receivedEventArgsDoor = null;
            _receivedEventArgsRFid = null;


            _door.DoorChangedEvent += (o, args) =>
            {
                _receivedEventArgsDoor = args;
            };

            _rfidReader.RFidChangedEvent += (o, args) =>
            {
                _receivedEventArgsRFid = args;
            };

        }


     
        
        [TestCase(false,true)]
        [TestCase(true,false)]
        public void HandleDoorChangedEvent_TestStates(bool preState,bool State)
        {
            if (preState == true)
            {
                _door.SetDoorState(preState);

            }
            _door.SetDoorState(State);

            Assert.That(_receivedEventArgsDoor, Is.Not.True);

        }

        [TestCase(true,false,123)]
        [TestCase(false, false, 323)]
        [TestCase(true, true, 23)]
        [TestCase(false, true, 13)]
        public void RFidDetectetEventChanged_TestStationStartCharge(bool Connection, bool Doorstate, int RFid)
        {


            _door.SetDoorState(Doorstate);
            _chargeControl.IsConnected().Returns(Connection);

            _rfidReader.SetID(RFid);

            Assert.That(_receivedEventArgsRFid,Is.Not.Null);



        }

        
        [TestCase(true, false, 123,123)]
        [TestCase(true, false, 456, 456)]
        [TestCase(true, false, 123,456)]
        [TestCase(true, false, 123, 786)]
        public void RFidDetectetEventChanged_TestStationEndCharge(bool Connection, bool Doorstate, int RFidStart, int RFidEnd)
        {

            _door.SetDoorState(Doorstate);
            _chargeControl.IsConnected().Returns(Connection);
            _rfidReader.SetID(RFidStart);

            _rfidReader.SetID(RFidEnd);



            Assert.That(_receivedEventArgsRFid, Is.Not.Null);



        }


    }
}
