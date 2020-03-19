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
        private DoorStateChangedEventArgs _receivedEventArgs;


        [SetUp]
        public void Setup()
        {
            _uut = new Door();

            _receivedEventArgs = null;

            _uut.DoorChangedEvent += (o, args) =>
            {
                _receivedEventArgs = args;
            };
        }


        [Test]
        public void DoorStateChangedEventCalledOnes()
        {
            _uut.SetDoorState(true);
            Assert.That(_receivedEventArgs, Is.Not.Null);

        }

    }
}
