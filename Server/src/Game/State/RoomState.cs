using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.src.server.Events;
using Shared.communication.enums;
using MazeServer.src.Data;
using MazeServer.src.engine;
using MazeServer.src.Factories.MapStructures;
using Shared.communication.ClientToServer;
using Shared.Enums;
using MazeServer.src.map.Generation.Parser;
using MazeServer.src.server;

namespace MazeServer.src.Game.State
{
    class RoomState: GameState
    {
        private readonly MapStyle DefaultMapStyle = MapStyle.Style1;
        private MapStyleFactory StyleFactory = new MapStyleFactory();
        private IStructureFactory SelectedStyle;
        private List<Connection> Connections = new List<Connection>();

        public RoomState(GameData data): base(data) { 
            this.SelectedStyle = this.StyleFactory.Create(this.DefaultMapStyle);
        }

        public override void HandleRequest(GameStateContext context, RequestReceivedArguments arguments)
        {
            Console.WriteLine(arguments.Request.RequestType);

            switch (arguments.Request.RequestType) 
            {
                case Requests.Initial:
                    this.Connections.Add(arguments.Connection);
                    break;
                case Requests.StartGame:
                    this.GenerateMap(this.SelectedStyle);
                    context.SetState(new PlayState(this.Data, this.Connections));

                    break;
                case Requests.SelectStyle:
                    this.SelectedStyle = this.StyleFactory.Create(((SelectStyleRequest) arguments.Request).Style);
                    break;
            }
        }

        private void GenerateMap(IStructureFactory structureFactory)
        {
            this.Data.Map.ForEach(s => s.Destroy());

            var parser = new MapParser();
            this.Data.Map = parser.Generate(structureFactory);
            this.Data.Map.ForEach(s => ((GameObject) s).Instantiate());
        }
    }
}
