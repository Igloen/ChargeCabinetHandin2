using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargeCabinetLibrary;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace ChargeCabinet.Test.Unit
{
    [TestFixture]
    public class TestConsoleWriter
    {
        private IConsoleWriter _uut;
        
        

        [SetUp]
       public void Setup()
       {
            _uut = new ConsoleWriter();
            

            
            
       }

      


   }
}
