using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.src.engine;

namespace MazeServer.src.map
{
    abstract class Structure: GameObject
    {
        public Structure Clone()
        {
            return (Structure) this.MemberwiseClone();
        }
    }
}
