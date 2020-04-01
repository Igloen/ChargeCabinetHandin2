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
        private IFileLogger _fileLogger;
        private IConsoleWriter _consoleWriter;

        [SetUp]
        public void Setup()
        {
           
           _rfidReader = Substitute.For<IRFidReader>();
           _door = Substitute.For<IDoor>();
           _fileLogger = Substitute.For<IFileLogger>();
            _usbCharger = Substitute.For<IUsbCharger>();
            _chargeControl = Substitute.For<IChargeControl>();
            _consoleWriter = Substitute.For<IConsoleWriter>();


            _uut = new StationControl(_door, _rfidReader, _chargeControl,_fileLogger,_consoleWriter);

           

        }



        [TestCase(true, false, true)]
        [TestCase(false, false,false)]
        [TestCase(false,true,false)]
        [TestCase(true,true,false)]
        public void HandleDoorChangedEvent_TestStates(bool stateDoor, bool stateLock, bool result)
        {
            _door.LockChangedEvent += Raise.EventWith(new LockStateChangedEventArgs() {StateLocked = stateLock});
            _door.DoorChangedEvent += Raise.EventWith( new DoorStateChangedEventArgs(){StateOpen = stateDoor});

            Assert.That(_uut._doorOpenState,Is.EqualTo(result));

        }

        [TestCase(true, true)]
        [TestCase(false, false)]
        public void HandleLockChangedEvent_TestStates( bool stateLock, bool result)
        {
            _door.LockChangedEvent += Raise.EventWith(new LockStateChangedEventArgs() { StateLocked = stateLock });

            Assert.That(_uut._doorLockState, Is.EqualTo(result));

        }

        [TestCase(true,false,123,1)]
        [TestCase(false, false, 323,0)]
        [TestCase(true, true, 23,2)]
        [TestCase(false, true, 13,2)]
        public void RFidDetectetEventChanged_TestStationStartCharge(bool Connection, bool Doorstate, int RFid, int result )
        {
            _chargeControl.IsConnected().Returns(Connection);
            
            _door.DoorChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs() { StateOpen = Doorstate });
            _rfidReader.RFidChangedEvent += Raise.EventWith(new RFidChangedEventArgs() {ID = RFid});

            Assert.That(_uut._stationState, Is.EqualTo(result));

        }

        
        [TestCase(123, 123,0)]
        [TestCase(123, 321, 1)]
        public void RFidDetectetEventChanged_TestStationEndCharge(int FirstID,int SecondID, int result)
        {

            _chargeControl.IsConnected().Returns(true);

            _door.DoorChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs() { StateOpen = false });
            _rfidReader.RFidChangedEvent += Raise.EventWith(new RFidChangedEventArgs() { ID = FirstID });

            _rfidReader.RFidChangedEvent += Raise.EventWith(new RFidChangedEventArgs() { ID = SecondID});


            Assert.That(_uut._stationState, Is.EqualTo(result));
        }


    }
}
