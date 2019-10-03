using Maze.Server.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Maze.Game.Objects.Map;

namespace Maze.Server.Factories.MapStructures
{
    class Style2StructureFactory: IStructureFactory
    {
        public Structure Create(Structures type)
        {
            switch (type)
            {
                case Structures.Wall:
                    return new BrickWall();
                case Structures.Floor:
                    return new CobblestoneFloor();
                default:
                    throw new NotImplementedException($"{type.ToString()} structure type is not supported");
            }
        }
    }
}
