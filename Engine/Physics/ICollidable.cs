using System;
using Win32;

namespace Maze.Engine.Physics
{
    public interface ICollidable
    {
        Mesh GetMesh();
        bool IsDynamic();
        void OnCollision(Collision collision);
    }
}
