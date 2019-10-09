using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Shared.communication;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Shared.communication.enums;
using MazeServer.src.server.Events;

namespace MazeServer.src.server
{
    public class ClientConnection
    {
        protected NetworkStream Stream { get; set; }
        protected BinaryFormatter Formatter = new BinaryFormatter();
        public ClientConnection(NetworkStream stream)
        {
            this.Stream = stream;
        }

        public void SendResponse(object data)
        {
            try {
                this.Formatter.Serialize(this.Stream, data);
            } catch (Exception e) {}
        }
    }
}
