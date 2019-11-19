using System;
using Xunit;
using Maze.Server.Factories.MapStructures;
using Maze.Game.Objects.Map;

namespace Maze.Tests.Factory
{
    public class Style1StructureFactoryTest
    {
        [Fact]
        public void Create()
        {         
            Style1StructureFactory obj = new Style1StructureFactory();
            var wall = obj.Create(Server.Enums.Structures.Wall);
            var floor = obj.Create(Server.Enums.Structures.Floor);
            Assert.IsType(wall.GetType(), new WoodenWall());
            Assert.IsType(floor.GetType(), new BrickFloor());
        }
    }
}
