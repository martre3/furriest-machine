using System;
using Win32;

namespace Maze.Engine.Physics
{
    public class Collision
    {
        public Mesh CollidedWith { get; }

        public Collision(Mesh collidedWith)
        {
            this.CollidedWith = collidedWith;
        }
    }
}
