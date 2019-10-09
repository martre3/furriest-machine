using System;
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

namespace MazeServer.src.Game.Players
{
    class PlayerHandler
    {
        private List<Player> Players = new List<Player>();
        private PlayerInitializer Initializer = new PlayerInitializer();
        private readonly SemaphoreSlim PlayerListLock = new SemaphoreSlim(1, 1);

        public PlayerHandler()
        {
            PlayerInitializer.PlayerCreated += AddPlayer;
            MazeServer.src.engine.Game.PostFrame += UpdatePlayers;
        }

        private async void AddPlayer(object sender, PlayerCreatedArguments arguments)
        {
            await this.PlayerListLock.WaitAsync();
            try
            {
                this.Players.Add(arguments.NewPlayer);
            }
            finally
            {
                this.PlayerListLock.Release();
            }
        }

        private async void UpdatePlayers(object sender, PostFrameArguments arguments)
        {
            Console.WriteLine(arguments.State.Objects.Count);
            List<Shared.Engine.GameObject> objects = arguments.State.Objects.Select(this.ToClientGameObject).ToList();

            await this.PlayerListLock.WaitAsync();
            try
            {
                this.Players.ForEach((player) => player.Connection.SendResponse(objects));
            }
            finally
            {
                this.PlayerListLock.Release();
            }
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
