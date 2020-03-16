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
    public class TestStationControl
    {
        private StationControl _uut;
        private IDoor _door;
        private IRFidReader _rfidReader;

        [SetUp]

        public void Setup()
        {
            _door = Substitute.For<IDoor>();
            _rfidReader = Substitute.For<IRFidReader>();

            _uut = new StationControl(_door, _rfidReader);
        }

    }
}
