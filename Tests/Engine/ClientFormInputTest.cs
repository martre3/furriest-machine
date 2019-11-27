using System;
using System.Collections.Generic;
using System.Text;
using Maze.Engine.Input;
using Xunit;
using System.Windows.Forms;

namespace Maze.Tests.Engine
{
    public class ClientFormInputTest
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Put(Keys key)
        {
            ClientFormInput input = new ClientFormInput();
            input.SetUserId(1);
            input.Initialize(1);
            input.KeyDown(key);
            var result = input.IsUserKeyDown(1, key);
            Assert.True(result);
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
            new object[] { Keys.Up },
            new object[] { Keys.Down  },
            new object[] { Keys.Left },
            new object[] { Keys.Right  },
            };
    }
}
