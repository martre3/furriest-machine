using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Game.Objects.Map
{
    [Serializable]

    public class BrickWall: Structure
    {
        public BrickWall()
        {
            this.TextureFile = "Wall2.tif";
        }
    }
}
