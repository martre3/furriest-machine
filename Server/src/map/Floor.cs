using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.src.map
{
    class Floor: Structure
    {
        public Floor()
        {
            this.TextureID = 1;
        }

        public override string TextureFile
        {
            get { return "Floors.tif"; }
        }
    }
}
