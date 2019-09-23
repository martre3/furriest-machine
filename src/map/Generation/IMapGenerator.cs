using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.src.engine;

namespace Maze.src.map.Generation
{
    interface IMapGenerator
    {
        List<GameObject> Generate();
    }
}
