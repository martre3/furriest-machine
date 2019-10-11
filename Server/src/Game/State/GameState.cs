using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.src.Data;
using MazeServer.src.engine;
using MazeServer.src.server.Events;

namespace MazeServer.src.Game.State
{
    abstract class GameState: IGameState
    {
        public GameData Data { get; set; }

        public GameState(GameData data)
        {
            this.Data = data;
        }

        public virtual void HandleRequest(GameStateContext context, RequestReceivedArguments arguments) {}
        public virtual void HandleUpdate(GameStateContext context) {}

        public object GetStateData()
        {
            return this.Data;
        }
    }
}
