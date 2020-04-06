using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ChargeCabinetLibrary;
using NSubstitute;

namespace ChargeCabinet.Test.Unit
{
    [TestFixture]

    public class TestDoor
    {
        private IDoor _uut;
        private DoorStateChangedEventArgs _receivedDoorEventArgs;
        private LockStateChangedEventArgs _receivedLockEventArgs;

        [SetUp]
        public void Setup()
        {
            _uut = new Door();

            _receivedDoorEventArgs = null;
            _receivedLockEventArgs = null;

            _uut.DoorChangedEvent += (o, args) =>
            {
                _receivedDoorEventArgs = args;
            };

            _uut.LockChangedEvent += (o, args) =>
            {
                _receivedLockEventArgs = args;
            };
        }


        [Test]
        public void DoorStateChangedEventCalledOnes()
        {
            _uut.SetDoorState(true);
            Assert.That(_receivedDoorEventArgs, Is.Not.Null);

        }

        [Test]
        public void LockDoorTest()
        {
            _uut.LockDoor();

            Assert.That(_receivedLockEventArgs.StateLocked, Is.True);
        }

        [Test]
        public void UnlockDoorTest()
        {
            _uut.UnlockDoor();

            Assert.That(_receivedLockEventArgs.StateLocked, Is.False);
        }

    }
}
