using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Maze.Server.Network
{
    public class Connection
    {    
        public int Id { get; }
        protected NetworkStream Stream { get; set; }
        protected BinaryFormatter Formatter = new BinaryFormatter();
        public Connection(int id, NetworkStream stream)
        {
            this.Id = id;
            this.Stream = stream;
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
