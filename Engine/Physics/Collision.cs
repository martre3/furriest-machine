using System;
using Win32;

namespace Maze.Engine.Physics
{
    public class Collision
    {
        public ICollidable CollidedWith { get; }

        public Collision(ICollidable collidedWith)
        {
            this.CollidedWith = collidedWith;
        }
    }
}
