using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ChargeCabinetLibrary
{
    public class FileLogger : IFileLogger
    {
        private string logFile = "logfile.txt";
        public void LogDoorLocked(int id)
        {
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
            }
        }

        public void LogDoorUnlocked(int id)
        {
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
            }
        }
    }
}
