using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Maze.Server.Game.Data;
using System.Drawing;
using Maze.Game.Objects;
using Maze.Server.Game;
using Maze.Game;
using Maze.Engine.Input;
using Maze.Game.Objects.PickUp;
using Maze.Game.Items;
using Maze.Game.Objects.Map;

namespace Maze.Tests.Game.Data
{
    public class GameDataTest
    {
        GameData obj = new GameData(new GameState(), new InputHandler(new FormInput()));
        [Fact]
        public void addPlayer()
        {
            obj.AddPlayer(new Player(new Point(), new Inventory()));
            Assert.Single(obj.Players);
        }

        [Fact]
        public void addFood()
        {
            obj.AddFood(new ItemPickup(new SpeedBoost(1f)));
            Assert.Single(obj.Food);
        }

        [Fact]
        public void addStructure()
        {
            obj.AddFood(new ItemPickup(new SpeedBoost(1f)));
            Assert.Single(obj.Food);
            Assert.NotNull(obj);
        }
    }
}
