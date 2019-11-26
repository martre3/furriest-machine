using System;
using System.Collections;
using Xunit;
using Maze.Server.Factories.MapStructures;
using Maze.Game.Objects.Map;
using System.Collections.Generic;

namespace Maze.Tests.Factory
{
    public class Style1StructureFactoryTest
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Create(Server.Enums.Structures obj1, object obj2)
        {
            Style1StructureFactory obj = new Style1StructureFactory();
            var result = obj.Create(obj1);
            Assert.IsType(result.GetType(), obj2);
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
            new object[] { Server.Enums.Structures.Floor, new BrickFloor() },
            new object[] { Server.Enums.Structures.Wall, new WoodenWall()  },
            };
        
    }
}
