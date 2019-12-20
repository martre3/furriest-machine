using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Maze.Game;
using Server.src.FileLogging;

namespace Server.src.singleton
{
    public class Logger
    {
        private static Logger instance;
        private Message Chain;
        private static object lockThread = new object();
        private string fileNameInfo = DateTime.Today.Date.ToString("yyyy-MM-dd") + "-info.log";
        private string fileNameError = DateTime.Today.Date.ToString("yyyy-MM-dd") + "-errors.log";
        private string fileNameWarning = DateTime.Today.Date.ToString("yyyy-MM-dd") + "-warnings.log";

        private Logger()
        {
            setChain();

            if (!File.Exists(fileNameInfo))
            {
                File.Create(fileNameInfo);
            }
            if (!File.Exists(fileNameError))
            {
                File.Create(fileNameError);
            }
            if (!File.Exists(fileNameWarning))
            {
                File.Create(fileNameWarning);
            }
        }

        private void setChain()
        {
            Chain = new ErrorMessageLogging();
            WarningMessageLogging Chain2 = new WarningMessageLogging();
            InfoMessageLogging Chain3 = new InfoMessageLogging();
            NullMessage Chain4 = new NullMessage();
            Chain.setNextChain(Chain2);
            Chain2.setNextChain(Chain3);
            Chain3.setNextChain(Chain4);
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

        public Message GetChain()
        {
            return this.Chain;
        }

        public void chooseFileAndWrite(string type, string text)
        {
            lock(lockThread)
            {
                if (type == "info")
                {
                    printToFile(fileNameInfo, text);
                }
                if (type == "error")
                {
                    printToFile(fileNameError, text);
                }
                if (type == "warning")
                {
                    printToFile(fileNameWarning, text);
                }
            }
            Console.WriteLine(text);
        }

        private void printToFile(string file, string text)
        {
            using (StreamWriter writer = File.AppendText(file))
            {
                writer.WriteLine(DateTime.Now.ToString() + ": " + text);
            }
        }
    }
}
