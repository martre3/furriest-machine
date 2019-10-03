using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.src.enums;
using MazeServer.src.map.Parser;
using MazeServer.src.map;
using MazeServer.src.engine.Events.Arguments;
using MazeServer.src.server;
using MazeServer.src.server.Events;
using MazeServer.src.Game.Players.Events;
using Shared.communication.enums;
using System.Timers;
using System.Net.Sockets;
using System.Threading;

namespace MazeServer.src.Game.Players
{
    class PlayerInitializer
    {
        public static EventHandler<PlayerCreatedArguments> PlayerCreated;
        public PlayerInitializer()
        {
            Server.RequestReceived += HandleRequest;
        }

        private void HandleRequest(object sender, RequestReceivedArguments arguments)
        {
            if (arguments.Request.RequestType == Request.Initial)
            {
                Random random = new Random();

                var player = new Player(arguments.Connection) {
                    X = random.Next(20, 500),
                    Y = random.Next(20, 500),
                };

                player.Instantiate();
                PlayerInitializer.PlayerCreated(this, new PlayerCreatedArguments(player));
            }
        }
    }
}
