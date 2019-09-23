using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Maze.src.map
{
    class Wall: Structure
    {
        public override string TextureFile
        {
            get { return "Wall.tif"; }
        }
    }
}
