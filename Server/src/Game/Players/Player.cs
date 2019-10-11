using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.src.enums;
using MazeServer.src.engine;
using MazeServer.src.map;
using MazeServer.src.engine.Events.Arguments;
using MazeServer.src.server;
using System.Timers;
using System.Net.Sockets;
using System.Threading;

namespace MazeServer.src.Game.Players
{
    class Player: GameObject
    {
        public ClientConnection Connection { get; }

        public Player(ClientConnection connection)
        {
            this.Connection = connection;
            this.TextureFile = "slime.png";
        }
    }
}
