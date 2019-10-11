using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.src.engine;
using MazeServer.src.Factories.MapStructures;

namespace MazeServer.src.map.Generation
{
    interface IMapGenerator
    {
        List<Structure> Generate(IStructureFactory factory);
    }
}
