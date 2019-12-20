using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Server.Network;
using Shared.communication.enums;
using System.Timers;
using System.Net.Sockets;
using System.Threading;
using Maze.Server.Game.Data;
using Maze.Game.Enums;

namespace Maze.Server.Players
{
    class PlayerHandler
    {
        private PlayerInitializer Initializer = new PlayerInitializer();
        private GameData MatchData;
        
        private bool _preySpawned = false;


        public PlayerHandler(GameData data)
        {
            this.MatchData = data; // pabaigti add Player
            // PlayerInitializer.PlayerCreated += AddPlayer;
            // MazeServer.src.engine.Game.PostFrame += UpdatePlayers;
        }

        // private async void AddPlayer(object sender, PlayerCreatedArguments arguments)
        public void AddPlayer(Connection playerConnection)
        {
            var player = this.Initializer.Create();
            player.UserId = playerConnection.Id;

            player.Role = _preySpawned ? PlayerRole.Seeker : PlayerRole.Prey; 
            _preySpawned = true;
            
            this.MatchData.AddPlayer(player);
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

        // public void UpdatePlayers(GameData data)
        // {
        //     data.ApplyToConnections(connection => connection.SendResponse(data.State));
        // }
    }
}
