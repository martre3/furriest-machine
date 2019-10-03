using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Shared.communication.enums;

namespace Shared.communication
{
    [Serializable]
    public class Request
    {
        public Requests RequestType { get; set; }
    }
}
