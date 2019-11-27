using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization;

namespace Maze.Server.Network
{
    public class Connection: IConnection
    {    
        public int Id { get; }
        protected Stream Stream { get; set; }
        protected IFormatter Formatter;
        public Connection(int id, Stream stream, IFormatter formatter)
        {
            this.Id = id;
            this.Stream = stream;
            this.Formatter = formatter;
        }

        public int GetId()
        {
            return this.Id;
        }

        public void SendResponse(object data)
        {
            try {
                this.Formatter.Serialize(this.Stream, data);
            } catch (Exception e) {}
        }

        public object GetRequest()
        {
            return this.Formatter.Deserialize(this.Stream);
        }
    }
}
