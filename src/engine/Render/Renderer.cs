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

namespace Maze.src.engine
{
    class Renderer
    {
        private readonly List<GameObject> Objects = new List<GameObject>();
        private readonly Canvas MainCanvas;

        public Renderer(Canvas canvas)
        {
            this.MainCanvas = canvas;

            GameObject.ObjectInstantiated += this.AddNewObject;
        }

        private void AddNewObject(object sender, GameObjectInitializedArguments e)
        {
            this.Objects.Add(e.Object);
        }

        public void Redraw()
        {
            this.MainCanvas.Children.Clear();

            this.Objects.ForEach(this.DrawObject);
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
