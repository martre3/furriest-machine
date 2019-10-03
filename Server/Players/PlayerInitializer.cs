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

            var player = new Player() {
                pos = new Point(random.Next(20, 500), random.Next(20, 500)),
                size = new Size(32, 32),
            };

            return player;
        }
    }
}
