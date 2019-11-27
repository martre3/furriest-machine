using System;
using Xunit;
using Maze.Server.Factories.MapStructures;
using Maze.Game.Objects.Map;
using System.Collections.Generic;
using Maze.Server.Enums;

namespace Maze.Tests.Factory
{
    public class Style2StructureFactoryTest
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Create(Structures obj1, object obj2)
        {
            Style2StructureFactory obj = new Style2StructureFactory();
            var result = obj.Create(obj1);
            Assert.IsType(result.GetType(), obj2);
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
            new object[] { Structures.Floor, new CobblestoneFloor() },
            new object[] { Structures.Wall, new BrickWall()  },
            };
    }
}
