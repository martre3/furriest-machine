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
    public class Connection: ClientConnection
    {
        public Connection(NetworkStream stream): base (stream) {}
        
        public object GetRequest()
        {
            return this.Formatter.Deserialize(this.Stream);
        }
    }
}
