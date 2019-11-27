using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Maze.Game.Objects;
using Maze.Game.Items;
using Maze.Game.Items.Bombs;

namespace Maze.Tests.Objects
{
    public class InventoryTest
    {

        [Theory]
        [MemberData(nameof(Data))]
        public void Put(IItem obj1)
        {
            Inventory inventory = new Inventory();
            bool result = inventory.Put(obj1);
            Assert.True(result);
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
            new object[] { new SpeedBoost(2.5f) },
            new object[] { new SpeedBoost(1f)  },
            new object[] { new BombAdapter(new FreezeBomb()) },
            new object[] { new BombAdapter(new SlowBomb())  },
            };
    }
}
