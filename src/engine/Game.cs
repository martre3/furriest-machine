using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;
using Maze.src.enums;
using Maze.src.map.Parser;
using System.Timers;
using Maze.src.map; // istrinti

namespace Maze.src.engine
{
    class Game
    {
        private int FramesRendered = 0;
        private Renderer RenderEngine;
        private List<GameObject> Map = new List<GameObject>();
        private Structure Player;

        public Game(Canvas canvas)
        {
            CompositionTarget.Rendering += this.GameLoop;
            this.RenderEngine = new Renderer(canvas);
            
            var fpsTimer = new Timer(1000) {
                AutoReset = true,
                Enabled = true,
            }; 
            fpsTimer.Elapsed += (object sender, ElapsedEventArgs e) => {
                Console.WriteLine($"{this.FramesRendered} fps");
                this.FramesRendered = 0;
            };
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (this.Player != null)
            {
                this.Player.X += 1;
            }

            if (this.Map.Count == 0)
            {
                var parser = new MapParser();
                this.Map = parser.Generate();
                this.Map.ForEach(s => s.Insantiate());

                this.Player = new StructureFactory().Create(Structures.Player);
                this.Player.Y = 32;

                this.Player.Insantiate();
            }

            this.RenderEngine.Redraw();

            this.FramesRendered++;
        }
    }
}
