using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Net.Sockets;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using Maze.Engine.Events;
using Maze.Engine.Input;
using Maze.Game.Assets;

namespace Maze.Game.Objects.Food
{
    [Serializable]
    public class BombPickup: Food
    {
        public BombPickup(Point position): base(position) {}
        
        public override void PickUp() 
        {

        }
    }
}
