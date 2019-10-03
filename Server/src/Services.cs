using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Maze.Server.Network;

namespace MazeServer.src
{
    class Services
    {
        Listener Server = new Listener(1239);
    }
}
