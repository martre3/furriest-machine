using System.Collections.Generic;
using System;

namespace Maze.Engine.Physics
{
    public class InterpolationEngine: SimulationEngine
    {
        public InterpolationEngine(ICollidableContainer container): base(container) {}
        
        protected sealed override void SimulateObject(ICollidable collider)
        {
            var thisMesh = collider.GetMesh();

            thisMesh.Position.X = this.Interpolate(thisMesh.Position.X, thisMesh.RealPosition.X);
            thisMesh.Position.Y = this.Interpolate(thisMesh.Position.Y, thisMesh.RealPosition.Y);
        }

        private int Interpolate(int first, int second)
        {
            return first + (second - first) / 5;
        }
    }
}
