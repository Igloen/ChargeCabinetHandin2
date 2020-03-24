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
    public class TestRFidReader
    {
        private IRFidReader _uut;
        private RFidChangedEventArgs _receivedEventArgs;


        [SetUp]
        public void SetUp()
        {

            _uut = new RFidReader();

            _receivedEventArgs = null;

            _uut.RFidChangedEvent += (o, args) =>
            {
                _receivedEventArgs = args;

            };
        }

        [TestCase(123)]
        [TestCase(321)]
        [TestCase(323)]
        public void RFidEventArgsIsNotNull(int id)
        {
            _uut.SetID(id);
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }


        [TestCase(345)]
        public void SetID_Test(int id)
        {
            _uut.SetID(id);
            Assert.That(_receivedEventArgs.ID, Is.EqualTo(id));
        }

    }
}
