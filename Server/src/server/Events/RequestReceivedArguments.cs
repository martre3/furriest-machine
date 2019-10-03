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
        public ClientToServer Request { get; }
        public ClientConnection Connection { get; }
        public RequestReceivedArguments(ClientToServer request, ClientConnection connection)
        {
            this.Request = request;
            this.Connection = connection;
        }
    }
}
