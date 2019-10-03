using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Maze.Server.Map.Generation;
using Maze.Server.Enums;
using Maze.Game.Objects.Map;
using Maze.Server.Factories.MapStructures;
using System.Drawing;

namespace Maze.Server.Map.Generation.Parser
{
    class MapParser: IMapGenerator
    {
        private readonly string ProjectDirectory = Environment.CurrentDirectory;

        public List<Structure> Generate(IStructureFactory factory)
        {
            List<Structure> structures = new List<Structure>();
            List<List<MapTile>> map = new List<List<MapTile>>(); 

            // return structures;

            int lineCount = 0;
            string line;

            StreamReader file = new StreamReader($"{ProjectDirectory}/assets/maps/testMap.txt");

            while ((line = file.ReadLine()) != null)
            {
                List<MapTile> row = new List<MapTile>();

                int columnCount = 0;

                foreach (char c in line)
                {
                    // row.Add(new MapTile(this.ParseChar(c), row.Count, map.Count));
                    structures.Add(this.CreateStructure(factory, this.ParseChar(c), columnCount, lineCount));
                    columnCount++;
                }

                // map.Add(row);
                lineCount++;
            }

            return structures;
        }

        // private List<GameObject> merge(List<List<MapTile>> map)
        // {
            
        // }

        private Structures ParseChar(char structure)
        {
            switch (structure)
            {
                case '#':
                    return Structures.Wall;
                case '=':
                    return Structures.Floor;
                default:
                    throw new NotImplementedException("Not supported character found in the map");
            }
        }

        private Structure CreateStructure(IStructureFactory factory, Structures structureType, int x, int y)
        {
            var wall = factory.Create(structureType);
            wall.pos = new Point(x * 32, y * 32);
            wall.size = new Size(32, 32);
            // wall.Height = height;
            // wall.Width = width;

            return wall;
        }
    }
}
