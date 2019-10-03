using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Maze.src.engine.Events.Arguments;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Drawing;
using System.Configuration;
using System.Data;
using System.Windows;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Threading;

namespace Maze.src.engine
{
    class Renderer
    {
        private readonly List<GameObject> Objects = new List<GameObject>();
        private readonly SemaphoreSlim ObjectListLock = new SemaphoreSlim(1, 1);

        private readonly Canvas MainCanvas;

        public Renderer(Canvas canvas)
        {
            this.MainCanvas = canvas;

            GameObject.ObjectInstantiated += this.AddNewObject;
        }

        private async void AddNewObject(object sender, GameObjectInitializedArguments e)
        {
            await this.ObjectListLock.WaitAsync();
            try
            {
                this.Objects.Add(e.Object);
            }
            finally
            {
                this.ObjectListLock.Release();
            }

            
        }

        public async void Redraw()
        {
            await this.ObjectListLock.WaitAsync();
            try
            {
                await Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.MainCanvas.Children.Clear();
                    this.Objects.ForEach(this.DrawObject);
                }));
            }
            finally
            {
                this.ObjectListLock.Release();
            }
        }

        private void DrawObject(GameObject gameObject)
        {
            ImageBrush colors_brush = new ImageBrush()
            {
                ImageSource = gameObject.Texture,
                TileMode = TileMode.Tile,
                Viewport = new Rect(0, 0, 32, 32),
                ViewportUnits = BrushMappingMode.Absolute,

            };

            var rectangle = new Rectangle()
            {
                Width = 32,
                Height = 32,
                Fill = colors_brush,
            };

            Canvas.SetLeft(rectangle, gameObject.X);
            Canvas.SetTop(rectangle, gameObject.Y);

            this.MainCanvas.Children.Add(rectangle);
        }
    }
}
