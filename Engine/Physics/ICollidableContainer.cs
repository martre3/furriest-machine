using System;
using Win32;
using System.Collections.Generic;

namespace Maze.Engine.Physics
{
    public interface ICollidableContainer
    {
        List<ICollidable> GetDynamicObjects();
        List<ICollidable> GetAllColliders();
    }
}
