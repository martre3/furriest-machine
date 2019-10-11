using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.src.Data;
using MazeServer.src.server.Events;

namespace MazeServer.src.engine
{
    interface IGameState
    {
        void HandleRequest(GameStateContext context, RequestReceivedArguments arguments);
        void HandleUpdate(GameStateContext context);
        object GetStateData();
    }
}
