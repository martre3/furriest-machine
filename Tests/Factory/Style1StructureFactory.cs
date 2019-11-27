using System;
using System.Collections;
using Xunit;
using Maze.Server.Factories.MapStructures;
using Maze.Game.Objects.Map;
using System.Collections.Generic;
using Maze.Server.Enums;

namespace Maze.Tests.Factory
{
    public class Style1StructureFactoryTest
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Create(Structures obj1, object obj2)
        {
            Style1StructureFactory obj = new Style1StructureFactory();
            var result = obj.Create(obj1);
            Assert.IsType(result.GetType(), obj2);
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
            new object[] { Structures.Floor, new BrickFloor() },
            new object[] { Structures.Wall, new WoodenWall()  },
            };
        
    }
}
