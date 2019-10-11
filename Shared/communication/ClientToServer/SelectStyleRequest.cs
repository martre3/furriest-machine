using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Shared.communication.enums;
using Shared.Engine;
using Shared.Enums;

namespace Shared.communication.ClientToServer
{
    [Serializable]
    public class SelectStyleRequest: Request
    {
        // public GameState State { get; set; }
        public MapStyle Style { get; set; }
    }
}
