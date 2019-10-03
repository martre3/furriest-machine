using System;
using System.Drawing;
using System.Windows.Forms;
using Maze.Engine;
using Maze.Engine.Input;
using Maze.Engine.Renderer;
using Maze.Game;

namespace Maze.Client
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var clientForm = new ClientForm()
            {
                ClientSize = new Size(800, 576),
                Text = "Maze™",
            };

            GDIRenderer renderer = new GDIRenderer(clientForm);
            ClientFormInput input = new ClientFormInput();

            HookInputEvents(clientForm, input);

            GameEngine engine = new GameEngine(renderer, input)
            {
                EnableRender = true,
                MinFrameTime = 16,
                UpdateTimeStep = 33,
            };

            var game = new GameRunner(engine, new ClientGameState());
            var synchronizer = new StateSynchronizer(game.GetGameState(), input);

            // TODO: Stop game thread and clean up when window starts closing
            game.RunAsync();
            Application.Run(clientForm);
        }

        private static void HookInputEvents(ClientForm form, ClientFormInput input)
        {
            form.KeyUp += (object sender, KeyEventArgs e) => {
                input.KeyUp(e.KeyCode);
            };

            form.KeyDown += (object sender, KeyEventArgs e) => {
                input.KeyDown(e.KeyCode);
            };
        }
    }
}
