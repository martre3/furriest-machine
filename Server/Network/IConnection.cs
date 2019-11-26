using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Maze.Server.Network
{
    public interface IConnection
    {    
        void SendResponse(object data);

        object GetRequest();
    }
}
