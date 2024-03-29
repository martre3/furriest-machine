﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Game;
using Maze.Game.Objects;
using Maze.Game.Objects.PickUp;
using Maze.Game.Objects.Map;
using Maze.Engine.Input;
using Maze.Server.Players;
using Maze.Server.Network;
using Maze.Server.Game.Mediators;

namespace Maze.Server.Game.Data
{
    public class GameData
    {
        public List<Structure> Map { get; set; }
        public List<Player> Players { get; set; }
        public List<Food> Food { get; set; }

        public GameState State { get; set; }

        private ConnectionsHandler ConnectionsHandler = new ConnectionsHandler();
        private InputHandler InputHandler;

        private BombMediator _bombMediator;

        public GameData(GameState state, InputHandler inputHandler)
        {
            this.State = state;
            this.Map = new List<Structure>();
            this.Players = new List<Player>();
            this.Food = new List<Food>();
            this._bombMediator = new BombMediator(this);
            this.InputHandler = inputHandler;
        }

        public GameData()
        {
        }

        public void DestroyObject(object sender, DestroyedGameObjectEventArgs args)
        {
            var gameObject = args.GameObject;

            if (gameObject is Structure)
            {
                this.Map.Remove((Structure) gameObject);
            }

            if (gameObject is Player)
            {
                this.Players.Remove((Player) gameObject);
            }

            if (gameObject is Food)
            {
                this.Food.Remove((Food) gameObject);
            }

            this.State.Destroy(gameObject);
        }

        public void AddObject(GameObject gameObject)
        {
            this.State.Register(gameObject);
        }

        public void AddStructure(Structure structure)
        {
            this.Map.Add(structure);
            this.State.Register(structure);
        }

        public void AddPlayer(Player player)
        {
            player.BombMediator = _bombMediator;
            this.Players.Add(player);
            this.State.Register(player);
            this.State.Register(player.Inventory);
        }

        public void AddFood(Food food)
        {
            this.Food.Add(food);
            this.State.Register(food);
        }

        public virtual bool IsFirstRequest(IConnection connection)
        {
            return this.ConnectionsHandler.IsInit(connection);
        }
        public virtual void InitializeConnection(IConnection connection)
        {
            this.ConnectionsHandler.Connect(connection);
        }

        public void ApplyToConnections(Action<IConnection> action)
        {
            this.ConnectionsHandler.Apply(action);
        }

        public void UpdateState()
        {
            this.ConnectionsHandler.Apply(connection => {
                this.State.UserId = connection.GetId();
                connection.SendResponse(this.State);
            });
        }

        public void UpdateInput(FormInput input)
        {
            this.InputHandler.Merge(input);
        }
    }
}
