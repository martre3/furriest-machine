using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.communication.enums;
using Maze.Server.Game.Data;
using Shared.Enums;
using Maze.Server.Network;
using Maze.Server.Players;
using Maze.Server.Events;
using Maze.Game.Objects.PickUp;
using Maze.Game.Objects;
using Maze.Game.Items;
using Maze.Game.Items.Bombs;
using Maze.Game.Enums;

namespace Maze.Server.Game.State
{

    class PlayState: GameState
    {
        private PlayerHandler PlayerHandler;
        private bool a = true;

        public PlayState(GameData data): base(data) { 
            this.PlayerHandler = new PlayerHandler(data);

            data.ApplyToConnections(connection => this.PlayerHandler.AddPlayer((Connection) connection));

            Console.WriteLine(data.State.GameObjects.Count);
        }

        public override void HandleRequest(GameStateContext context, RequestReceivedArguments arguments)
        {
            Data.UpdateInput(arguments.Input);
        }

        public override void HandleUpdate(GameStateContext context)
        {
            var prey = this.Data.Players.Find(player => player.Role == PlayerRole.Prey);

            if (prey != null)
            {
                foreach (var player in this.Data.Players)
                {
                    if (prey == player)
                    {
                        continue;
                    }

                    if (prey.GetDistanceTo(player) < 100)
                    {
                        prey.Health -= 4f;
                    }

                    if (prey.Health <= 0)
                    {
                        context.SetState(new EndGameState(this.Data));
                    }
                }
            }

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
