using System;

namespace Maze.Game.Objects.Map
{
    [Serializable]

    public class RedCobblestoneFloor: Structure
    {
        public RedCobblestoneFloor(): base()
        {
            this.TextureFile = "Floors5.tif";
            _mesh.IsCollider = false;
        }
    }
}
