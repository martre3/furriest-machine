using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Shared.communication.enums;
using Maze.Server.Game.Data;
using Maze.Server.Factories.MapStructures;
using Shared.Enums;
using Maze.Server.Map.Generation.Parser;
using Maze.Server.Network;
using Maze.Server.Events;
using System.Windows.Forms;
using Maze.Game.Objects.GUI;
using Maze.Game.Enums;
using Maze.Engine.Events;

namespace Maze.Server.Game.State
{
    public class PreGameState: GameState
    {
        private double _timer = 5000;

        private CountDownGUI _gui;
        
        public PreGameState(GameData data): base(data) { 
            _gui = new CountDownGUI() {
                CurrentTime = _timer,
            };

            data.AddObject(_gui);
        }

        public override void HandleUpdate(GameStateContext context, UpdateEventArgs args)
        {
            _timer -= args.LastFrameTime;

            if (_timer <= 0) 
            {
                _gui.Destroy();
                context.SetState(new PlayState(this.Data));
            }

            _gui.CurrentTime = _timer / 1000;
            Data.UpdateState();
        }
    }
}
