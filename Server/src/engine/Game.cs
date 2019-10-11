using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.src.enums;
using MazeServer.src.map.Generation.Parser;
using MazeServer.src.map;
using MazeServer.src.engine.Events.Arguments;
using System.Timers;
using System.Net.Sockets;
using System.Threading;
using Shared.Engine;
using Shared.communication.enums;
using MazeServer.src.Factories.MapStructures;

namespace MazeServer.src.engine
{
    class Game
    {
        public static event EventHandler<EventArgs> PreFrame;
        public static event EventHandler<EventArgs> NextFrame;
        public static event EventHandler<PostFrameArguments> PostFrame;

        private List<Structure> Map = new List<Structure>();
        private List<GameObject> Objects = new List<GameObject>();
        private MapStyleFactory StyleFactory = new MapStyleFactory();
        private GameStateContext GameState;

        public Game(IGameState initialState)
        {
            GameObject.ObjectInstantiated += NewGameObject;
            GameObject.ObjectDestroyed += DestroyedGameObject;

            this.GameState = new GameStateContext(initialState);
            this.StartGameLoop();
        }

        private void StartGameLoop()
        {
            while (true)
            {
                // Game.PreFrame?.Invoke(this, EventArgs.Empty);
                // Game.NextFrame?.Invoke(this, EventArgs.Empty);
                // Game.PostFrame?.Invoke(this, new PostFrameArguments(new GameState(this.Objects.Select(o => (Shared.Engine.GameObject) o).ToList()))); // temporary solution, until 'engine' will be moved to Shared
                
                Thread.Sleep(200);
            }
        }

        private void NewGameObject(object sender, GameObjectInitializedArguments e)
        {
            this.Objects.Add(e.Object);
        }

        private void DestroyedGameObject(object sender, GameObjectInitializedArguments e)
        {
            this.Objects.Remove(e.Object);
        }
    }
}
