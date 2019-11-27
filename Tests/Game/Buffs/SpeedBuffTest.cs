using System;
using System.Collections.Generic;
using System.Text;
using Maze.Game.Items;
using Xunit;
using Maze.Game.Objects;
using System.Drawing;
using Moq;

namespace Maze.Tests.Game.Buffs
{
    public class SpeedBuffTest
    {       

        [Theory]
        [MemberData(nameof(Data))]
        public void Create(float speed)
        {
            var player = new Player(Point.Empty, Mock.Of<Inventory>());
            SpeedBuff obj = new SpeedBuff(speed);
            obj.Apply(player);
            Assert.Equal(player.SpeedMultiplier, speed);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void Undo(float speed)
        {
            var player = new Player(Point.Empty, Mock.Of<Inventory>());
            SpeedBuff obj = new SpeedBuff(speed);
            obj.Apply(player);
            obj.Undo(player);
            Assert.Equal(player.SpeedMultiplier, 1);
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { 2.5f },
                new object[] { 5f  },
                new object[] { 10.1f },
                new object[] { 3.6f  },
            };
    }
}
