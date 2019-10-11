using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Shared.communication.enums;
using Shared.Engine;

namespace Shared.communication.ServerToClient
{
    [Serializable]
    public class RedrawRequest: Request
    {
        // public GameState State { get; set; }
    }
}
