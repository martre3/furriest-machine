using System;
using Xunit;
using Maze.Server.Factories.MapStructures;
using Maze.Game.Objects.Map;
using System.Collections.Generic;

namespace Maze.Tests.Factory
{
    public class Style2StructureFactoryTest
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Create(Server.Enums.Structures obj1, object obj2)
        {
            Style2StructureFactory obj = new Style2StructureFactory();
            var result = obj.Create(obj1);
            Assert.IsType(result.GetType(), obj2);
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
            new object[] { Server.Enums.Structures.Floor, new CobblestoneFloor() },
            new object[] { Server.Enums.Structures.Wall, new BrickWall()  },
            };
    }
}
