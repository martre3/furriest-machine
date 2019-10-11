using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.src.Data;
using MazeServer.src.server;
using MazeServer.src.server.Events;

namespace MazeServer.src.engine
{
    class GameStateContext
    {
        private IGameState CurrentState;

        public GameStateContext(IGameState initialState) 
        {
            this.CurrentState = initialState;
            Server.RequestReceived += this.HandleRequest;   
        }

        private void HandleRequest(object sender, RequestReceivedArguments arguments)
        {
            this.CurrentState.HandleRequest(this, arguments);
        }

        public void Update()
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
