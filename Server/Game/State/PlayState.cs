using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.communication.enums;
using Maze.Server.Game.Data;
using Shared.communication.ClientToServer;
using Shared.Enums;
using Maze.Server.Network;
using Maze.Server.Players;
using Maze.Server.Events;

namespace Maze.Server.Game.State
{

    class PlayState: GameState
    {
        private PlayerHandler PlayerHandler;

        public PlayState(GameData data): base(data) { 
            this.PlayerHandler = new PlayerHandler(data);

            data.ApplyToConnections(connection => this.PlayerHandler.AddPlayer(connection));
        }

        public override void HandleRequest(GameStateContext context, RequestReceivedArguments arguments)
        {
            Data.UpdateInput(arguments.Input);
            // switch (arguments.Request.RequestType) 
            // {
            //     case Requests.Initial:
            //         this.GenerateMap(this.SelectedStyle);
            //         context.SetState(new GameState(this.Data));
            //         break;
            //     case Requests.SelectStyle:
            //         this.SelectedStyle = this.StyleFactory.Create(((SelectStyleRequest) arguments.Request).Style);
            //         break;
            // }
        }

        public override void HandleUpdate(GameStateContext context)
        {
            this.Data.UpdateState();
            // this.PlayerHandler.UpdatePlayers(this.Data);t
        }
    }
}
