using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Maze.src.engine;

namespace Maze.src.map
{
    abstract class Structure: GameObject
    {
        public abstract string TextureFile { get; }

        public Structure Clone()
        {
            return (Structure) this.MemberwiseClone();
        }
    }
}
