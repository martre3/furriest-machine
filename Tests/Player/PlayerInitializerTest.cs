using System;
using System.Collections.Generic;
using System.Text;
using Maze.Server.Players;
using Maze.Game.Objects;
using Xunit;

namespace Maze.Tests.PlayerInitializerTest
{
    public class PlayerInitializerTest
    {
        PlayerInitializer obj = new PlayerInitializer();

        [Fact]
        public void create() 
        {
            var result = obj.Create();
            Assert.NotNull(result.GetType());
        }
    }
}
