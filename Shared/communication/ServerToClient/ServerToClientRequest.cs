using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Shared.communication.enums;
using Shared.Engine;
using System.Collections.Generic;

namespace Shared.communication.ServerToClient
{
    [Serializable]
    public class ServerToClientRequest: Request
    {
        // public GameState State { get; set; }
        public List<GameObject> Objects { get; set; }
    }
}
