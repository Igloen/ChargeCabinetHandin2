using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargeCabinetLibrary;
using NUnit.Framework;

namespace ChargeCabinet.Test.Unit
{
    [TestFixture]
    public class TestFileLogger
    {
        private IFileLogger _uut;

       [SetUp]
       public void Setup()
       {
            _uut = new FileLogger();

       }

       [Test]
       public void FileLog_testLogDoorLocked()
       {

            _uut.LogDoorLocked(123);

            //Assert.That();


       }




   }
}
