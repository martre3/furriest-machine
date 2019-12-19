using System.Collections.Generic;
using Maze.Game.Objects.Map;

namespace Maze.Server.Map.Generation
{
    interface IMapGenerator
    {
        List<Structure> Generate(MapContext context);
    }
}
