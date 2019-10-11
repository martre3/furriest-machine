using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.src.engine.Events.Arguments;
using System.Drawing;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Windows;
using System.IO;
using System.Reflection;
using System.Threading;
using FormWindow;

namespace Maze.src.engine
{
    class Renderer
    {
        private readonly BlockingCollection<GameObject> Objects = new BlockingCollection<GameObject>();
        private BlockingCollection<(Brush, Rectangle)> t = new BlockingCollection<(Brush, Rectangle)>();
        private readonly SemaphoreSlim ObjectListLock = new SemaphoreSlim(1, 1);
        private Random random = new Random();

        private readonly GraphicsForm Graphics;

        public Renderer(GraphicsForm graphics)
        {
            this.Graphics = graphics;

            GameObject.ObjectInstantiated += this.AddNewObject;
        }

        private void AddNewObject(object sender, GameObjectInitializedArguments e)
        {
            this.Objects.Add(e.Object);
            this.t.Add(((Brush) new TextureBrush(e.Object.Texture), this.GetDrawable(e.Object)));
            // this.t.Add(((Brush) new SolidBrush(Color.FromArgb(random.Next(256), random.Next(256), random.Next(256))), this.GetDrawable(e.Object)));
        }

        public void Redraw()
        {
            //  var tuple = this.Objects.Select(gameObject => ()).ToList();
        
             this.Graphics.Paint(this.t.ToList());
        }

        private Rectangle GetDrawable(GameObject gameObject)
        {
            // ImageBrush colors_brush = new ImageBrush()
            // {
            //     ImageSource = gameObject.Texture,
            //     TileMode = TileMode.Tile,
            //     Viewport = new Rect(0, 0, 32, 32),
            //     ViewportUnits = BrushMappingMode.Absolute,

            // };

            return new Rectangle()
            {
                Width = 32,
                Height = 32,
                X = gameObject.X * 32,
                Y = gameObject.Y * 32,
                // Fill = colors_brush,
            };

            // Canvas.SetLeft(rectangle, gameObject.X);
            // Canvas.SetTop(rectangle, gameObject.Y);

            // this.Graphics.Paint(rectangle);
        }
    }
}
