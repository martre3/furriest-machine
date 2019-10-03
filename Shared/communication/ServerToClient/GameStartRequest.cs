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
    public class GameStartRequest: ServerToClientRequest
    {
        public List<GameObject> Objects { get; set; }
        public List<GameObject> Players { get; set; }
    }
}
