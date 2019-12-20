using System;
using System.Collections.Generic;
using System.Text;

namespace Server.src.FileLogging
{
    public class NullMessage : Message
    {
        public override void PrintMessageToFile(string message)
        {
            // Do nothing.
        }

        public override void setLogger(Message obj)
        {
            // Do nothing.
        }

        public override void setNextChain(Message obj)
        {
            // Do nothing.
        }
    }
}
