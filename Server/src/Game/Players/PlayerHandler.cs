using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.src.enums;
using MazeServer.src.engine.Events.Arguments;
using MazeServer.src.engine;
using MazeServer.src.server;
using MazeServer.src.Game.Players.Events;
using System.Timers;
using System.Net.Sockets;
using System.Threading;
using MazeServer.src.Data;

namespace MazeServer.src.Game.Players
{
    class PlayerHandler
    {
        private BlockingCollection<Player> Players = new BlockingCollection<Player>();
        private PlayerInitializer Initializer = new PlayerInitializer();
        private readonly SemaphoreSlim PlayerListLock = new SemaphoreSlim(1, 1);

        public PlayerHandler()
        {
            // PlayerInitializer.PlayerCreated += AddPlayer;
            // MazeServer.src.engine.Game.PostFrame += UpdatePlayers;
        }

        // private async void AddPlayer(object sender, PlayerCreatedArguments arguments)
        public void AddPlayer(Player player)
        {
            this.Players.Add(player);
            // await this.PlayerListLock.WaitAsync();
            // try
            // {
            //     this.Players.Add(arguments.NewPlayer);
            // }
            // finally
            // {
            //     this.PlayerListLock.Release();
            // }
        }

        public void UpdatePlayers(GameData data)
        {
            List<Shared.Engine.GameObject> objects = data.Objects.Select(this.ToClientGameObject).ToList();

            this.Players.ToList().ForEach((player) => player.Connection.SendResponse(objects));
        }

        private Shared.Engine.GameObject ToClientGameObject(GameObject gameObject)
        {
            return new Shared.Engine.GameObject() {
                X = gameObject.X,
                Y = gameObject.Y,
                TextureFile = gameObject.TextureFile,
            };
        }
    }
}
