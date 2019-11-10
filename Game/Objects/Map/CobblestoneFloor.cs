using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Game.Objects.Map
{
    [Serializable]
    public class CobblestoneFloor: Structure
    {
        public CobblestoneFloor(): base()
        {
            this.TextureFile = "Floors4.tif";
            _mesh.IsCollider = false;
        }
    }
}
