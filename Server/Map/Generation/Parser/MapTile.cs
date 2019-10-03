using Maze.Server.Enums;

namespace Maze.Server.Map.Generation.Parser
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
