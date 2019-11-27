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
            var player = new Mock<Player>(Point.Empty, Mock.Of<Inventory>());
            SpeedBuff obj = new SpeedBuff(speed);
            obj.Apply(player.Object);
            player.VerifySet(p => p.SpeedMultiplier = (int) speed);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void Undo(float speed)
        {
            Mock<Inventory> inventory = new Mock<Inventory>();
            var player = new Mock<Player>(Point.Empty, Mock.Of<Inventory>());
            int currentSpeed = player.Object.SpeedMultiplier;
            SpeedBuff obj = new SpeedBuff(speed);
            obj.Apply(player.Object);
            obj.Undo(player.Object);
            player.VerifySet(p => p.SpeedMultiplier = currentSpeed);
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
