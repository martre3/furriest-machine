using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Server.Network;
using System.Timers;
using System.Net.Sockets;
using System.Threading;
using System.Drawing;
using Maze.Game.Objects;

namespace  Maze.Server.Players
{
    class PlayerInitializer
    {
        public Player Create()
        {
            Random random = new Random();

            // var player = new Player(new Point(random.Next(100, 500), random.Next(20, 500)));
            var player = new Player(new Point(36, 36));

            return player;
        }
    }
}
