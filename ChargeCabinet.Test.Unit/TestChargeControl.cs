using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            _usbCharger = Substitute.For<IUsbCharger>();

            _uut = new ChargeControl(_usbCharger); 
        } 

        [Test]
        public void testCurrentChange()
        {
            _usbCharger.CurrentValue = 55; 

            Assert.That(_uut., is.equalTo(_uut.ChargeControlState.Chargeing))

        }



    }
}
