using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Maze.Server.Enums;
using Maze.Game.Objects.Map;

namespace Maze.Server.Factories.MapStructures
{
    interface IStructureFactory
    {
        Structure Create(Structures type);
    }
}
