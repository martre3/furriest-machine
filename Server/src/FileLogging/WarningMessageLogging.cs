using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Server.src.singleton;
using System.IO;

namespace Server.src.FileLogging
{
    public class WarningMessageLogging : Message
    {
        private static object lockThread = new object();
        private string fileNameWarning = DateTime.Today.Date.ToString("yyyy-MM-dd") + "-warnings.log";
        public WarningMessageLogging() { }

        public override void PrintMessageToFile(string message)
        {
            string[] parts = message.Split(' ');
            if (parts.Contains("warning"))
            {
                write(message);
            }
            else
            {
                this.Next.PrintMessageToFile(message);
            }
        }

        private void write(string text)
        {
            lock (lockThread)
            {
                using (StreamWriter writer = File.AppendText(fileNameWarning))
                {
                    writer.WriteLine(DateTime.Now.ToString() + ": " + text);
                }
            }
            Console.WriteLine(text);
        }

        public override void setNextChain(Message obj)
        {
            this.Next = obj;
        }
    }
}
