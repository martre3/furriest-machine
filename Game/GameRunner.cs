using System.Threading.Tasks;
using Maze.Engine;
using Maze.Engine.Events;
using Maze.Game.Objects;

namespace Maze.Game
{
    public class GameRunner
    {
        private GameEngine _engine;
        private GameState _state;
        private Diagnostics _diagnostics;

        public GameRunner(GameEngine engine)
        {
            _engine = engine;
            _state = new GameState();
            _diagnostics = new Diagnostics();

            _state.Register(new Example());

            engine.OnInit += HandleInit;
            engine.OnUpdate += HandleUpdate;
            engine.OnFrame += HandleFrame;
        }

        public void RunAsync()
        {
            Task.Run(_engine.Loop);
        }

        private void HandleInit(object sender, InitEventArgs e)
        {
            _state.GameObjects.ForEach(o => {
                o.InitializeAssets();
            });
        }

        private void HandleUpdate(object sender, UpdateEventArgs e)
        {
            _state.GameObjects.ForEach(o => {
                o.Update(_engine.Input, e);
            });

            _diagnostics.Update(_engine.Input, e);
        }

        private void HandleFrame(object sender, FrameEventArgs e)
        {
            _state.GameObjects.ForEach(o => {
                o.Draw(e.Surface);
            });

            _diagnostics.Draw(e.Surface);
        }
    }
}
