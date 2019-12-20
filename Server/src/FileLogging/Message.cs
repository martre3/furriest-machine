using System;
using System.Collections.Generic;
using System.Text;
using Server.src.singleton;

namespace Server.src.FileLogging
{
    public abstract class Message
    {
        public Message Next;

        public virtual void PrintMessageToFile(string message) { }

        public virtual void setNextChain(Message obj) { }

        public virtual void setLogger(Message obj) { }


    }
}
