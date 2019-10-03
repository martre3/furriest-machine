using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Game;
using Maze.Game.Objects;
using Maze.Game.Objects.Map;
using Maze.Engine.Input;
using Maze.Server.Players;
using Maze.Server.Network;

namespace Maze.Server.Game.Data
{
    class GameData
    {
        public List<Structure> Map { get; set; }
        public List<Player> Players { get; set; }

        private ConnectionsHandler ConnectionsHandler = new ConnectionsHandler();
        private InputHandler InputHandler;

        public GameState State { get; set; }

        public GameData(GameState state, InputHandler inputHandler)
        {
            this.State = state;
            this.Map = new List<Structure>();
            this.Players = new List<Player>();
            this.InputHandler = inputHandler;
        }

        public void AddStructure(Structure structure)
        {
            this.Map.Add(structure);
            this.State.Register(structure);
        }

        public void AddPlayer(Player player)
        {
            this.Players.Add(player);
            this.State.Register(player);
        }

        public bool IsFirstRequest(Connection connection)
        {
            return this.ConnectionsHandler.IsInit(connection);
        }
        public void InitializeConnection(Connection connection)
        {
            this.ConnectionsHandler.Connect(connection);
        }

        public void ApplyToConnections(Action<Connection> action)
        {
            this.ConnectionsHandler.Apply(action);
        }

        public void UpdateState()
        {
            this.ConnectionsHandler.Apply(connection => {
                this.State.UserId = connection.Id; // TODO: This is a stupid idea
                connection.SendResponse(this.State);
            });
        }

        public void UpdateInput(FormInput input)
        {
            this.InputHandler.Merge(input);
        }
    }
}
