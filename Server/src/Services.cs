using MazeServer.src.map;
using MazeServer.src.enums;
using MazeServer.src.Game.Players;
using MazeServer.src.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MazeServer.src
{
    class Services
    {
        PlayerHandler PlayerHandler = new PlayerHandler();
        Server Server = new Server(1239);
    }
}
