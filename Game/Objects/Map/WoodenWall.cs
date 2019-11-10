using System;
using System.Drawing;

namespace Maze.Game.Objects.Map
{
    [Serializable]
    public class WoodenWall: Structure
    {
        public WoodenWall(): base()
        {
            this.TextureFile = "Wall.tif";
        }
    }
}
