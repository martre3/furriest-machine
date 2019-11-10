using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Maze.Server.Game.State;
using Maze.Server.Game.Data;
using MazeServer.src;
using Maze.Engine;
using Maze.Engine.Input;
using Maze.Game;
using Maze.Server.Game;
using Maze.Engine.Physics;

namespace MazeServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var state = new Maze.Game.GameState();

            FormInput input = new FormInput();
            PhysicsEngine physicsEngine = new PhysicsEngine(state);
            GameEngine engine = new GameEngine(input, physicsEngine)
            {
                EnableRender = false,
                MinFrameTime = 16,
                UpdateTimeStep = 33,
            };

            var game = new GameRunner(engine, state);

            // TODO: Stop game thread and clean up when window starts closing
            game.RunAsync();

            new Services();
            new GameStateContext(new RoomState(new GameData(game.GetGameState(), new InputHandler(input))));

            Console.ReadKey(true);
        }
    }
}
