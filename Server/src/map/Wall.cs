﻿using System;
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
            this.TextureFile = "Wall.tif";
        }
    }
}