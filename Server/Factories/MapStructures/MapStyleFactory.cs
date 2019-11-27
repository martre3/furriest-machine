using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Shared.Enums;

namespace Maze.Server.Factories.MapStructures
{
    public class MapStyleFactory
    {
        public IStructureFactory Create(MapStyle style)
        {
            switch (style)
            {
                case MapStyle.Style1:
                    return new Style1StructureFactory();
                case MapStyle.Style2:
                    return new Style2StructureFactory();
                default:
                    throw new NotImplementedException($"{style.ToString()} structure type is not supported");
            }
        }
    }
}
