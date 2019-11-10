using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Game.Objects.Map
{
    [Serializable]
    public class RedBrickWall: Structure
    {
        public RedBrickWall(): base()
        {
            this.TextureFile = "Wall5.tif";
        }
    }
}
