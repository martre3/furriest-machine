using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Server.Events;
using Maze.Engine.Events;

namespace Maze.Server.Game.State
{
    public interface IGameState
    {
        void HandleRequest(GameStateContext context, RequestReceivedArguments arguments);
        void HandleUpdate(GameStateContext context, UpdateEventArgs args);
        void HandleUpdate(GameStateContext context);
        object GetStateData();
    }
}
