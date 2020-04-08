using System;
using System.Collections.Generic;
using System.IO;
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
        private string logFile = "logfile.txt";

        [SetUp]
       public void Setup()
       {
            _uut = new FileLogger();

       }

       [TestCase(123)]
       [TestCase(12346)]
       [TestCase(345)]
        public void FileLog_testLogDoorLocked_fileWriter(int id)
       {

            _uut.LogDoorLocked(id);

            DateTime dato = DateTime.Now;

            var reader = File.ReadLines(logFile).Last();

            string result = dato + ": Skab låst med RFID: " + id;

            Assert.That(reader,Is.EqualTo(result));


       }

        [TestCase(123)]
        [TestCase(12346)]
        [TestCase(345)]
        public void FileLog_testLogDoorUnLocked_fileWriter(int id)
        {

            _uut.LogDoorUnlocked(id);

            DateTime dato = DateTime.Now;

            var reader = File.ReadLines(logFile).Last();

            string result = dato + ": Skab låst op med RFID: " + id;

            Assert.That(reader, Is.EqualTo(result));


        }



    }
}
