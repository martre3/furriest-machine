using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Server.Events;

namespace Maze.Server.Game.State
{
    interface IGameState
    {
        void HandleRequest(GameStateContext context, RequestReceivedArguments arguments);
        void HandleUpdate(GameStateContext context);
        object GetStateData();
    }
}
