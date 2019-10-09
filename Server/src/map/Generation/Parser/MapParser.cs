using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MazeServer.src.map.Generation;
using MazeServer.src.engine;
using MazeServer.src.enums;

namespace MazeServer.src.map.Parser
{
    class MapParser: IMapGenerator
    {
        private readonly string ProjectDirectory = Environment.CurrentDirectory;

        public List<GameObject> Generate()
        {
            List<GameObject> structures = new List<GameObject>();
            StructureFactory factory = new StructureFactory();
            List<List<MapTile>> map = new List<List<MapTile>>(); 

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

                Console.WriteLine(structures.Count);
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

        private Structure CreateStructure(StructureFactory factory, Structures structureType, int x, int y)
        {
            var wall = factory.Create(structureType);
            wall.X = x;
            wall.Y = y;
            // wall.Height = height;
            // wall.Width = width;

            return wall;
        }
    }
}
