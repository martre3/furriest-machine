using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Game.Objects.Map
{
    [Serializable]
    public class BrickFloor: Structure
    {
        public BrickFloor(): base()
        {
            this.TextureFile = "Floors.tif";
            _mesh.IsCollider = false;
        }
    }
}
