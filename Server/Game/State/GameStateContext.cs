using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Server.Game.Data;
using Maze.Server.Events;
using Maze.Engine;
using Maze.Server.Network;

namespace Maze.Server.Game.State
{
    class GameStateContext
    {
        private IGameState CurrentState;

        public GameStateContext(IGameState initialState) 
        {
            this.CurrentState = initialState;
            GameEngine.PostUpdate += this.Update;   
            Listener.RequestReceived += this.HandleRequest;
        }

        public void HandleRequest(object sender, RequestReceivedArguments args)
        {
            this.CurrentState.HandleRequest(this, args);
        }

        public void Update(object sender, EventArgs args)
        {
            this.CurrentState.HandleUpdate(this);
        }

        public T GetStateData<T>()
        {
            return (T) this.CurrentState.GetStateData();
        }  

        public void SetState(IGameState newState)
        {
            this.CurrentState = newState;
        }
    }
}
