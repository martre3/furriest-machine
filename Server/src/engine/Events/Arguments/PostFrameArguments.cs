using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.src.engine;

namespace MazeServer.src.engine.Events.Arguments
{
    class PostFrameArguments: EventArgs
    {
        public GameState State { get; }

        public PostFrameArguments(GameState state)
        {
            this.State = state;
        }
    }
}
