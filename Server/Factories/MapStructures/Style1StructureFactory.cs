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
    public class Style1StructureFactory: IStructureFactory
    {
        public Structure Create(Structures type)
        {
            switch (type)
            {
                case Structures.Wall:
                    return new WoodenWall();
                case Structures.Floor:
                    return new BrickFloor();
                default:
                    throw new NotImplementedException($"{type.ToString()} structure type is not supported");
            }
        }
    }
}
