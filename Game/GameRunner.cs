using System.Threading.Tasks;
using Maze.Engine;
using Maze.Engine.Events;
using Maze.Game.Objects;
using System;

namespace Maze.Game
{
    public class GameRunner
    {
        private GameEngine _engine;
        private GameState _state;
        private Diagnostics _diagnostics;

        public GameRunner(GameEngine engine, GameState state)
        {
            _engine = engine;
            _state = state;

            _diagnostics = new Diagnostics();

            // _state.Register(new Example());

            engine.OnInit += HandleInit;
            engine.OnUpdate += HandleUpdate;
            engine.OnFrame += HandleFrame;
        }

        public void RunAsync()
        {
            Task.Run(_engine.Loop);
        }

        public GameState GetGameState()
        {
            return _state;
        }

        private void HandleInit(object sender, InitEventArgs e)
        {
            // _state.ApplyCallback(gameObject => gameObject.InitializeAssets(new AssetsLoader()));

            // _state.GameObjects.ForEach(o => {
            //     o.InitializeAssets();
            // });
        }

        private void HandleUpdate(object sender, UpdateEventArgs e)
        {
            try {
                _state.ApplyCallback(gameObject => {
                    if (!gameObject.IsDestroying && !gameObject.IsDestroyed)
                    {
                        gameObject.Update(_engine.Input, e, (GameState) _state.Clone());
                    }
                });

                // _state.GameObjects.ForEach(o => {
                //     o.Update(_engine.Input, e);
                // });

                _diagnostics.Update(_engine.Input, e);
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
        }

        private void HandleFrame(object sender, FrameEventArgs e)
        {
            _state.ApplyCallback(gameObject => {
                if (!gameObject.IsDestroying && !gameObject.IsDestroyed)
                {
                    gameObject.Draw(e.Surface);
                }
            });

            // _state.GameObjects.ForEach(o => {
            //     o.Draw(e.Surface);
            // });

            _diagnostics.Draw(e.Surface);
        }
    }
}
