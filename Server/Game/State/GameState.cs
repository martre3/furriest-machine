using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Server.Game.Data;
using Maze.Server.Events;
using Maze.Server.Game.State;

namespace Maze.Server.Game.State
{
   public abstract class GameState: IGameState
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
