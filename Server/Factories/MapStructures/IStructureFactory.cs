using System.Drawing;
using Maze.Server.Enums;
using Maze.Game.Objects.Map;

namespace Maze.Server.Factories.MapStructures
{
    public interface IStructureFactory
    {
        Structure Create(Structures type);
    }
}
