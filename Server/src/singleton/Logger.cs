using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Server.Map.Generation.Parser;

namespace Server.src.singleton
{
    class Logger
    {
        private static Logger instance;
        private static object lockThread = new object();
        private string fileName = DateTime.Today.Date.ToString("yyyy-MM-dd") + ".log";

        private Logger() {
            Console.WriteLine(DateTime.Today.Date.ToString("yyyy-MM-dd") + ".log");
            //Console.WriteLine(File.Exists(fileName));
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
                    MapGenerator mapGenerator = new MapGenerator();
                    mapGenerator.generateMatrixList();
                    var aa = mapGenerator.getGeneratedMatrix();
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
            Console.WriteLine(File.Exists(fileName));
            lock (lockThread)
            {
                using (StreamWriter writer = File.AppendText(fileName))
                {
                    //mapGenerator.printTofile(aa);
                    writer.WriteLine(DateTime.Now.ToString() + ": " + text);
                }
            }

            Console.WriteLine(text);
        }
    }
}
