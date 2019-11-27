using System;
using Xunit;
using Maze.Server.Game.State;
using Maze.Engine.Input;
using System.Net.Sockets;
using Maze.Server.Network;
using Moq;
using System.IO;
using Xunit.Abstractions;
using System.Runtime.Serialization;

namespace Maze.Tests.Game.Server.Network
{
    public class ConnectionsHandlerTest
    {
        private ConnectionsHandler _handler;

        private readonly ITestOutputHelper _testOutputHelper;

        public ConnectionsHandlerTest(ITestOutputHelper testOutputHelper)
        {
            _handler = new ConnectionsHandler();
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TestItAddsNewConnection()
        {
            var connection = new Mock<Connection>(1, Mock.Of<Stream>(), Mock.Of<IFormatter>());

            Assert.True(_handler.IsInit(connection.Object));
            _handler.Connect(connection.Object);
            Assert.False(_handler.IsInit(connection.Object));
        }

        [Fact]
        public void TestItAppliesCallbackToAllConnetions()
        {
            var data = Mock.Of<object>();

            var connection1 = new Mock<IConnection>();
            connection1.Setup(c => c.GetId()).Returns(1);
            var connection2 = new Mock<IConnection>();
            connection2.Setup(c => c.GetId()).Returns(2);

            _handler.Connect(connection1.Object);
            _handler.Connect(connection2.Object);

            _handler.Apply(c => c.SendResponse(data));
            connection1.Verify(c => c.SendResponse(data), Times.Once);
            connection2.Verify(c => c.SendResponse(data), Times.Once);
        }
    }
}
