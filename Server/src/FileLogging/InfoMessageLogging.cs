using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Server.src.singleton;
using System.IO;

namespace Server.src.FileLogging
{
    public class InfoMessageLogging : Message
    {
        private static object lockThread = new object();
        private string fileNameInfo = DateTime.Today.Date.ToString("yyyy-MM-dd") + "-info.log";
        public InfoMessageLogging() { }


        public override void PrintMessageToFile(string message)
        {
            string[] parts = message.Split(' ');
            if (!parts.Contains("error") && !parts.Contains("exception") && !parts.Contains("warning"))
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
                using (StreamWriter writer = File.AppendText(fileNameInfo))
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
