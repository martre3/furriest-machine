using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Game.Objects.Map
{
    [Serializable]

    public class RedCobblestoneFloor: Structure
    {
        public RedCobblestoneFloor()
        {
            this.TextureFile = "Floors5.tif";
        }
    }
}
