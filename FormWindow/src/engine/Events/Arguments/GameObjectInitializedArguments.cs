using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.src.engine.Events.Arguments
{
    class GameObjectInitializedArguments: EventArgs
    {
        public GameObject Object { get; }

        public GameObjectInitializedArguments(GameObject gameObject)
        {
            this.Object = gameObject;
        }
    }
}
