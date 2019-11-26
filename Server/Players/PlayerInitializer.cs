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
    public class PlayerInitializer
    {
        int count = 1;

        public Player Create()
        {
            Random random = new Random();

            // var player = new Player(new Point(random.Next(100, 500), random.Next(20, 500)));
            var player = new Player(new Point(36 * count++, 36), new Inventory());

            return player;
        }
    }
}
