using System;
using Xunit;
using Maze.Server.Game.State;
using Maze.Engine.Input;
using System.Net.Sockets;
using Maze.Server.Network;
using Moq;
using System.IO;
using Xunit.Abstractions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Maze.Tests.Server.Network
{
    public class ConnectionTest
    {
        private Connection _connection;
        private Mock<IFormatter> _formatter;
        private Stream _stream;

        public ConnectionTest()
        {
            _formatter = new Mock<IFormatter>();
            _stream = Mock.Of<Stream>();
            _connection = new Connection(1, _stream, _formatter.Object);
        }

        [Fact]
        public void TestItSendsResponse()
        {
            _connection.SendResponse(new object());
            _formatter.Verify(f => f.Serialize(_stream, It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public void TestItGetsRequest()
        {
            var result = Mock.Of<object>();

            _formatter.Setup(f => f.Deserialize(_stream)).Returns(result);
            Assert.Equal(result, _connection.GetRequest());
        }
    }
}
