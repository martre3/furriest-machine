using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.src.map
{
    class Wall: Structure
    {
        public Wall()
        {
            this.TextureID = 2;
        }
        public override string TextureFile
        {
            get { return "Wall.tif"; }
        }
    }
}
