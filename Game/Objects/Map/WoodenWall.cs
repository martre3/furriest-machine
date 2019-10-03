using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Game.Objects.Map
{
    [Serializable]
    public class WoodenWall: Structure
    {
        public WoodenWall()
        {
            this.TextureFile = "Wall.tif";
        }
    }
}
