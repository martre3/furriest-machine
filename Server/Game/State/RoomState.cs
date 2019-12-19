using System;
using Maze.Server.Game.Data;
using Shared.Enums;
using Maze.Server.Events;
using System.Windows.Forms;
using Maze.Game.Objects.RoomGUI;
using Maze.Server.Map.Generation;

namespace Maze.Server.Game.State
{
    public class RoomState: GameState
    {
        private MapGenerator _mapGenerator = new MapGenerator();
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
                    GenerateMap(new MapContext(SelectedStyle));
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

        private void GenerateMap(MapContext context)
        {
            _mapGenerator.Generate(context).ForEach(s => this.Data.AddStructure(s));
        }
    }
}
