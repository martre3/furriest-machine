using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.src.Game.Players.Events
{
    class PlayerCreatedArguments: EventArgs
    {
        public Player NewPlayer { get; }

        public PlayerCreatedArguments(Player player)
        {
            this.NewPlayer = player;
        }
    }
}
