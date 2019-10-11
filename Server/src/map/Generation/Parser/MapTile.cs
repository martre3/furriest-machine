using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MazeServer.src.map.Generation;
using MazeServer.src.engine;
using MazeServer.src.enums;

namespace MazeServer.src.map.Generation.Parser
{
    class MapTile
    {
        public Structures Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Merged { get; set; }

        public MapTile(Structures type, int x, int y)
        {
            this.Type = type;
            this.X = x;
            this.Y = y;
            this.Merged = false;
        }
    }
}
