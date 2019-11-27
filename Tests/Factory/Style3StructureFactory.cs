using System;
using Xunit;
using Maze.Server.Factories.MapStructures;
using Maze.Game.Objects.Map;

namespace Maze.Tests.Factory
{
    public class Style3StructureFactoryTest
    {
        [Fact]
        public void Create()
        {
            Style3StructureFactory obj = new Style3StructureFactory();
            var wall = obj.Create(Maze.Server.Enums.Structures.Wall);
            var floor = obj.Create(Maze.Server.Enums.Structures.Floor);
            Assert.IsType(wall.GetType(), new RedBrickWall());
            Assert.IsType(floor.GetType(), new RedCobblestoneFloor());
        }
    }
}
