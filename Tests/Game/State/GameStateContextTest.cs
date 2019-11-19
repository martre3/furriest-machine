using System;
using Xunit;
using System.Collections.Generic;
using System.Text;
using Maze.Game;
using Maze.Server.Game.State;
using Maze.Server.Game.Data;
using Maze.Server.Game;
using Maze.Engine.Input;
using Maze.Server.Events;
using Maze.Server.Network;
using System.Net.Sockets;

namespace Maze.Tests.Game.State
{
    public class GameStateContextTest
    {
        GameStateContext obj = new GameStateContext(new RoomState(new GameData(new Maze.Game.GameState(), new InputHandler(new FormInput()))));
        RequestReceivedArguments arguments = new RequestReceivedArguments(new FormInput(), new Connection(0, new NetworkStream(new Socket(new SocketInformation))));
        [Fact]
        public void test()
        {
            obj.HandleRequest(obj, arguments);
            Console.WriteLine("SDADAS");
        }
    }
}
