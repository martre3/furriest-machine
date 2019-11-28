using System;
using Xunit;
using Maze.Server.Game.State;
using Maze.Engine.Input;
using Maze.Server.Events;
using Maze.Server.Network;
using Moq;

namespace Maze.Tests.Game.State
{
    public class GameStateContextTest
    {
        private GameStateContext _context;
        private Mock<IGameState> _state;

        public GameStateContextTest()
        {
            _state = new Mock<IGameState>();
            _context = new GameStateContext(_state.Object);
        }

        [Fact]
        public void TestCurrentStateHandlesRequest()
        {
            var args = new Mock<RequestReceivedArguments>(Mock.Of<FormInput>(), Mock.Of<IConnection>());
            _context.HandleRequest(this, args.Object);
            _state.Verify(mock => mock.HandleRequest(_context, args.Object), Times.Once());
        }

        [Fact]
        public void TestCurrentStateHandlesUpdate()
        {
            var args = new Mock<EventArgs>();
            _context.Update(this, args.Object);
            _state.Verify(mock => mock.HandleUpdate(_context), Times.AtLeast(1));
        }
    }
}
