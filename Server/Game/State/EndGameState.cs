using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Shared.communication.enums;
using Maze.Server.Game.Data;
using Maze.Server.Factories.MapStructures;
using Shared.communication.ClientToServer;
using Shared.communication.ServerToClient;
using Shared.Enums;
using Maze.Server.Map.Generation.Parser;
using Maze.Server.Network;
using Maze.Server.Events;
using System.Windows.Forms;
using Maze.Game.Objects.GUI;
using Maze.Game.Enums;

namespace Maze.Server.Game.State
{
    public class EndGameState: GameState
    {
        public EndGameState(GameData data): base(data) { 
            var gui = new EndGameGUI() {
                UsersWon = data.Players.Where(p => p.Role == PlayerRole.Seeker).Select(p => p.UserId).ToList(),
            };

            data.AddObject(gui);
        }

        public override void HandleUpdate(GameStateContext context)
        {
            Data.UpdateState();
        }
    }
}
