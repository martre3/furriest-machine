using System.Collections.Generic;
using System;

namespace Maze.Engine.Physics
{
    public class PhysicsEngine: SimulationEngine
    {
        public PhysicsEngine(ICollidableContainer container): base(container) {}
        
        protected sealed override void SimulateObject(ICollidable collider)
        {
            var thisMesh = collider.GetMesh();
            var direction = thisMesh.GetDirection();

            foreach (var c in _container.GetAllColliders()) // TODO get rid of nested loop
            {
                if (c == collider || !c.GetMesh().IsCollider)
                {
                    continue;
                }

                var collided = false;
                var tries = 1;

                while (thisMesh.IsColliding(c.GetMesh()) || c.GetMesh().IsColliding(thisMesh))
                {
                    collided = true;

                    if (c.GetMesh().IsTrigger)
                    {
                        break;
                    }

                    direction.X *= (int) 0.9;
                    direction.Y *= (int) 0.9;
                    tries--;

                    if (tries == 0)
                    {
                        break;
                    }
                }

                if (collided == true)
                {
                    collider.OnCollision(new Collision(c));
                }
            }

            thisMesh.Position.X += direction.X;
            thisMesh.Position.Y += direction.Y;
            thisMesh.ResetDirection();
        }
    }
}
