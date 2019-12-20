using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.src.singleton;
using System.IO;

namespace Server.src.FileLogging
{
    public class ErrorMessageLogging : Message
    {
        private string Message;
        private static object lockThread = new object();
        private string fileNameError = DateTime.Today.Date.ToString("yyyy-MM-dd") + "-errors.log";
        public ErrorMessageLogging() { }

        public ErrorMessageLogging(string message)
        {
            this.Message = message;
        }

        public string getMessage()
        {
            return this.Message;
        }

        public override void PrintMessageToFile(string message)
        {
            string[] parts = message.Split(' ');
            if (parts.Contains("error") || parts.Contains("exception"))
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
                using (StreamWriter writer = File.AppendText(fileNameError))
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
