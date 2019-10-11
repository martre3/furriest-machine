using MazeServer.src.map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MazeServer.src.enums;

namespace MazeServer.src.Factories.MapStructures
{
    interface IStructureFactory
    {
        Structure Create(Structures type);
    }
}
