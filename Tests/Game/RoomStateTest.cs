using System;
using Xunit;
using Maze.Server.Game.State;
using Maze.Engine.Input;
using Maze.Server.Events;
using Maze.Server.Network;
using Moq;
using Maze.Server.Game.Data;
using Maze.Server.Game;
using Maze.Game;
using System.Collections;
using System.Collections.Generic;
using Maze.Server.Map.Generation.Parser;
using Maze.Server.Factories.MapStructures;
using Maze.Game.Objects.Map;

namespace Maze.Tests.Game.State
{
    public class RoomStateTest
    {
        private RoomState _state;
        private Mock<GameStateContext> _context;
        private Mock<GameData> _data;

        private Mock<IConnection> _connection;

        private Mock<FormInput> _input;

        private Mock<RequestReceivedArguments> _args;

        public RoomStateTest()
        {
            _data = new Mock<GameData>(Mock.Of<Maze.Game.GameState>(), (new Mock<InputHandler>(Mock.Of<FormInput>()).Object));
            _state = new RoomState(_data.Object);
            _context = new Mock<GameStateContext>(_state);
            _connection = new Mock<IConnection>();
            _input = new Mock<FormInput>();
            _args = new Mock<RequestReceivedArguments>(_input.Object, _connection.Object);
        }

        [Fact]
        public void TestItInitializesConnectionOnFirstRequest()
        {
            _data.Setup(d => d.IsFirstRequest(_connection.Object)).Returns(true);
            _state.HandleRequest(_context.Object, _args.Object);
            _data.Verify(d => d.InitializeConnection(_connection.Object), Times.Once);
        }

        [Fact]
        public void TestItDoesNotInitializeOnSecondRequest()
        {
            var args = new Mock<RequestReceivedArguments>(Mock.Of<FormInput>(), _connection.Object);

            _data.Setup(d => d.IsFirstRequest(_connection.Object)).Returns(false);
            _data.Verify(d => d.InitializeConnection(_connection.Object), Times.Never);
        }

        // [Fact]
        // public void TestItGeneratesMapOnEnterClick()
        // {
        //     var mapStyleFactory = new Mock<MapStyleFactory>();
        //     var parser = new Mock<MapParser>();
        //     var factory = Mock.Of<IStructureFactory>();
        //     var structure = Mock.Of<BrickWall>();

        //     _state.Parser = parser.Object;
        //     _state.StyleFactory = mapStyleFactory.Object;

        //     _input.Setup(i => i.IsKeyDown(System.Windows.Forms.Keys.Enter)).Returns(true);

        //     mapStyleFactory.Setup(f => f.Create(Shared.Enums.MapStyle.Style1)).Returns(factory);

        //     parser.Setup(p => p.Generate(factory)).Returns(new List<Structure>() {structure});

        //     _data.Setup(d => d.IsFirstRequest(_connection.Object)).Returns(false);
        //     _data.Verify(d => d.InitializeConnection(_connection.Object), Times.Never);
        // }
    }
}
