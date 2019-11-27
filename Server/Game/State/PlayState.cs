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
using Maze.Game.Objects.PickUp;
using Maze.Game.Objects;
using Maze.Game.Items;
using Maze.Game.Items.Bombs;

namespace Maze.Server.Game.State
{

    class PlayState: GameState
    {
        private PlayerHandler PlayerHandler;
        private bool a = true;

        public PlayState(GameData data): base(data) { 
            this.PlayerHandler = new PlayerHandler(data);

            data.ApplyToConnections(connection => this.PlayerHandler.AddPlayer((Connection) connection));
        }

        public override void HandleRequest(GameStateContext context, RequestReceivedArguments arguments)
        {
            Data.UpdateInput(arguments.Input);
        }

        public override void HandleUpdate(GameStateContext context)
        {
            if (this.a) 
            {
                Random random = new Random();
                this.Data.AddFood(new ItemPickup(new BombAdapter(new FreezeBomb())) {
                    Position = new System.Drawing.Point(90, 32),
                });

                this.Data.AddFood(new ItemPickup(new SpeedBoost(2f)) {
                    Position = new System.Drawing.Point(128, 32),
                });

                this.Data.AddFood(new ItemPickup(new SpeedBoost(1f)) {
                    Position = new System.Drawing.Point(128, 32),
                });

                this.a = false;
            }

            this.Data.UpdateState();
            // this.PlayerHandler.UpdatePlayers(this.Data);t
        }
    }
}
