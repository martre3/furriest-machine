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
using Maze.Game.Objects.Food;
using System.Drawing;

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
            if (this.Data.Food.Count == 0) 
            {
                Random random = new Random();
                this.Data.AddFood(new BombPickup(new Point(random.Next(1, 500), random.Next(1, 500))));
            }

            this.Data.UpdateState();
            // this.PlayerHandler.UpdatePlayers(this.Data);t
        }
    }
}
