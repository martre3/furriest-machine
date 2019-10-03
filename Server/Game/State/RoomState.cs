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

namespace Maze.Server.Game.State
{
    class RoomState: GameState
    {
        private MapStyleFactory StyleFactory = new MapStyleFactory();
        private MapStyle SelectedStyle = MapStyle.Style1;
        public RoomState(GameData data): base(data) { }

        public override void HandleRequest(GameStateContext context, RequestReceivedArguments arguments)
        {
            if (Data.IsFirstRequest(arguments.Connection)) {
                Data.InitializeConnection(arguments.Connection);
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
