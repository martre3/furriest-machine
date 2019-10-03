using System;
using System.Collections.Generic;
using Maze.Game.Objects.Map;
using Maze.Server.Factories.MapStructures;

namespace Maze.Server.Map.Generation
{
    interface IMapGenerator
    {
        List<Structure> Generate(IStructureFactory factory);
    }
}
