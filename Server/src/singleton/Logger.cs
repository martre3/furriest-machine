using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Server.src.singleton
{
    class Logger
    {
        private static Logger instance;
        private static object lockThread = new object();
        private string fileName = DateTime.Today.Date.ToString("yyyy-MM-dd") + ".log";

        private Logger() {
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
            }
        }

        public static Logger getInstance()
        {
            if (instance == null)
            {
                lock (lockThread)
                {
                    if (instance == null)
                    {
                        instance = new Logger();
                    }
                }
            }
            return instance;
        }

        public void printToFile(string text)
        {
            lock(lockThread)
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.WriteLine(DateTime.Now.ToString() + ": " + text);
                }
            }

            Console.WriteLine(text);
        }
    }
}
