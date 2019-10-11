using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.communication;

namespace MazeServer.src.server.Events
{
    public class RequestReceivedArguments: EventArgs
    {
        public Request Request { get; }
        public Connection Connection { get; }
        public RequestReceivedArguments(Request request, Connection connection)
        {
            this.Request = request;
            this.Connection = connection;
        }
    }
}
