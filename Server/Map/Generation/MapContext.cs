using System.Collections.Generic;
using System.Drawing;
using Maze.Game.Objects.Map;
using Maze.Server.Enums;
using Maze.Server.Factories.MapStructures;
using Shared.Enums;

namespace Maze.Server.Map.Generation
{
    public class MapContext
    {
        private IStructureFactory _factory;
        private MapStyleFactory _styleFactory;

        public List<Structure> Structures { get; private set; }

        public MapContext(MapStyle style = MapStyle.Style1)
        {
            _styleFactory = new MapStyleFactory();
            _factory = _styleFactory.Create(style);
            Reset();
        }

        public void SetStyle(MapStyle style)
        {
            _factory = _styleFactory.Create(style);
        }

        public void AddStructure(Structures structureType, int xPos, int yPos)
        {
            var wall = _factory.Create(structureType);

            wall.Position = new Point(xPos * 32, yPos * 32);
            wall.size = new Size(32, 32);

            Structures.Add(wall);
        }

        public void Reset()
        {
            Structures = new List<Structure>();
        }
    }
}
