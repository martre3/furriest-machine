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
using Shared.communication.ServerToClient;
using Shared.Enums;
using MazeServer.src.map.Generation.Parser;
using MazeServer.src.server;

namespace MazeServer.src.Game.State
{
    class RoomState: GameState
    {
        private MapStyleFactory StyleFactory = new MapStyleFactory();
        private MapStyle SelectedStyle = MapStyle.Style1;
        private List<Connection> Connections = new List<Connection>();

        public RoomState(GameData data): base(data) { }

        public override void HandleRequest(GameStateContext context, RequestReceivedArguments arguments)
        {
            Console.WriteLine(arguments.Request.RequestType);

            switch (arguments.Request.RequestType) 
            {
                case Requests.Initial:
                    this.Connections.Add(arguments.Connection);
                    break;
                case Requests.StartGame:
                    this.GenerateMap(this.StyleFactory.Create(this.SelectedStyle));
                    context.SetState(new PlayState(this.Data, this.Connections));
                    break;
                case Requests.SelectStyle:
                    this.SelectedStyle = ((SelectStyleRequest) arguments.Request).Style;
                    break;
            }
        }

        public override void HandleUpdate(GameStateContext context)
        {
            this.Connections.ForEach(connection => connection.SendResponse(new RoomInfoRequest() {
                RequestType = ServerRequests.RoomInfo,
                SelectedStyle = this.SelectedStyle.ToString(),
            }));
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
