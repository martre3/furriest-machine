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
using MazeServer.src.Game.Players;
using MazeServer.src.server;

namespace MazeServer.src.Game.State
{

    class PlayState: GameState
    {
        private PlayerInitializer PlayerInitializer = new PlayerInitializer();
        private PlayerHandler PlayerHandler = new PlayerHandler();

        public PlayState(GameData data, List<Connection> connections): base(data) { 
            Console.WriteLine("Created");

            connections.ForEach(connection => this.PlayerHandler.AddPlayer(this.PlayerInitializer.Create(connection)));
            Console.WriteLine(connections.Count);
            this.PlayerHandler.UpdatePlayers(data);
            Console.WriteLine(connections.Count);
        }

        public override void HandleRequest(GameStateContext context, RequestReceivedArguments arguments)
        {
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
    }
}
