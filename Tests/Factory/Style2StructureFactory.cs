using System;
using Xunit;
using Maze.Server.Factories.MapStructures;
using Maze.Game.Objects.Map;

namespace Maze.Tests.Factory
{
    public class Style2StructureFactoryTest
    {
        [Fact]
        public void Create()
        {
            Style2StructureFactory obj = new Style2StructureFactory();
            var wall = obj.Create(Maze.Server.Enums.Structures.Wall);
            var floor = obj.Create(Maze.Server.Enums.Structures.Floor);
            Assert.IsType(wall.GetType(), new BrickWall());
            Assert.IsType(floor.GetType(), new CobblestoneFloor());
        }
    }
}
