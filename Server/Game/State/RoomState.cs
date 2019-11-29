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
using Maze.Game.Objects.RoomGUI;

namespace Maze.Server.Game.State
{
    public class RoomState: GameState
    {
        public MapStyleFactory StyleFactory = new MapStyleFactory();
        public MapParser Parser = new MapParser();
        private MapStyle SelectedStyle = MapStyle.Style1;
        
        private PlayersConnectedGUI _playersConnectedGUI;

        public RoomState(GameData data): base(data) { 
            _playersConnectedGUI = new PlayersConnectedGUI();
            data.AddObject(_playersConnectedGUI);
        }

        public override void HandleRequest(GameStateContext context, RequestReceivedArguments arguments)
        {
            if (Data.IsFirstRequest(arguments.Connection)) {
                Data.InitializeConnection(arguments.Connection);
                _playersConnectedGUI.PlayerNames.Add(String.Format("Player {0}", arguments.Connection.GetId()));
            }

            try {
                if (arguments.Input.IsKeyDown(Keys.Enter)) {
                    this.GenerateMap(this.StyleFactory.Create(this.SelectedStyle));
                    context.SetState(new PlayState(this.Data));
                }
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }

        public override void HandleUpdate(GameStateContext context)
        {
            Data.UpdateState();
        }

        private void GenerateMap(IStructureFactory structureFactory)
        {
            var parser = new MapParser();
            parser.Generate(structureFactory).ForEach(s => this.Data.AddStructure(s));
        }
    }
}
