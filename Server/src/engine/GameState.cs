using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.src.engine.Events.Arguments;

namespace MazeServer.src.engine
{
    class GameState
    {
        public List<GameObject> Objects { get; set; }

        public GameState(List<GameObject> objects)
        {
            this.Objects = objects;
        }
    }
}
