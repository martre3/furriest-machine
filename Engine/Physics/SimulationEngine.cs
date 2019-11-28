using System.Collections.Generic;
using System;

namespace Maze.Engine.Physics
{
    abstract public class SimulationEngine
    {
        protected ICollidableContainer _container { get; }

        public SimulationEngine(ICollidableContainer container)
        {
            _container = container;
        }

        public void Simulate()
        {
            try {
                foreach(var o in _container.GetDynamicObjects())
                {
                   this.SimulateObject(o);
                }
            } catch (Exception e) {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }

        abstract protected void SimulateObject(ICollidable collider);
    }
}
