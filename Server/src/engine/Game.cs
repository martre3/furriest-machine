using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.src.enums;
using MazeServer.src.map.Parser;
using MazeServer.src.map;
using MazeServer.src.engine.Events.Arguments;
using System.Timers;
using System.Net.Sockets;
using System.Threading;

namespace MazeServer.src.engine
{
    class Game
    {
        public static event EventHandler<EventArgs> PreFrame;
        public static event EventHandler<EventArgs> NextFrame;
        public static event EventHandler<PostFrameArguments> PostFrame;

        private List<GameObject> Map = new List<GameObject>();
        private List<GameObject> Objects = new List<GameObject>();

        public Game()
        {
            GameObject.ObjectInstantiated += NewGameObject;
            this.GenerateMap();
            this.StartGameLoop();
        }

        private void StartGameLoop()
        {
            while (true)
            {
                Game.PreFrame?.Invoke(this, EventArgs.Empty);
                Game.NextFrame?.Invoke(this, EventArgs.Empty);
                Game.PostFrame?.Invoke(this, new PostFrameArguments(new GameState(this.Objects)));
                Thread.Sleep(1000);
            }
        }

        private void GenerateMap()
        {
            var parser = new MapParser();
            this.Map = parser.Generate();
            this.Map.ForEach(s => s.Instantiate());
        }

        private void NewGameObject(object sender, GameObjectInitializedArguments e)
        {
            this.Objects.Add(e.Object);
        }
    }
}
