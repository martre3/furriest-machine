using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Reflection;
using Maze.src.engine;

namespace Maze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Canvas canvas = new Canvas();
        private Game GameEngine;

        public MainWindow()
        {
            InitializeComponent();
            this.Content = canvas;
            this.GameEngine = new Game(this.canvas);

            // canvas.Loaded += this.OnCanvasLoaded;
        }

        //private void OnCanvasLoaded(object sender, RoutedEventArgs e)
        //{

        //    ImageBrush colors_brush = new ImageBrush()
        //    {
        //        ImageSource =
        //        new BitmapImage(new Uri($"{projectdirectory}/assets/floors.tif", UriKind.Absolute)),
        //        // Stretch = Stretch.Uniform,
        //        TileMode = TileMode.Tile,
        //        Viewport = new Rect(0, 0, 32, 32),
        //        ViewportUnits = BrushMappingMode.Absolute,

        //    };

        //    var rectangle = new Rectangle()
        //    {
        //        Width = 320,
        //        Height = 32,
        //        Fill = colors_brush,
        //    };

        //    Canvas.SetLeft(rectangle, 32);

        //    this.canvas.Children.Add(rectangle);
        //    this.InvalidateVisual();
        //}

        //    private void Form1_Load()
        //    {
        //        try
        //        {
        //            string projectdirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;

        //            ImageBrush colors_brush = new ImageBrush();
        //            colors_brush.ImageSource =
        //                new BitmapImage(new Uri($"{projectdirectory}/assets/floors.tif", UriKind.Absolute));

        //            GeometryDrawing aGeometryDrawing = new GeometryDrawing()
        //            {
        //                Geometry = new RectangleGeometry(new Rect(32, 32, 320, 32)),
        //                Brush = colors_brush,
        //            };

        //            var rectangle = new Rectangle() {
        //                Width = 320,
        //                Height = 32,

        //            };

        //            this.canvas.Children.Add(rectangle);

        //            //mainPanel.Children.Add(myCanvas); ;

        //            //TextureBrush floorBrush = new TextureBrush(floors)
        //            //{
        //            //    WrapMode = System.Drawing.Drawing2D.WrapMode.Tile
        //            //};

        //            //Bitmap walls = (Bitmap)Image.FromFile($"{projectdirectory}/assets/Wall.tif", true);

        //            //TextureBrush wallsBrush = new TextureBrush(walls)
        //            //{
        //            //    WrapMode = System.Drawing.Drawing2D.WrapMode.Tile
        //            //};

        //            //Graphics formgraphics = this.CreateGraphics();

        //            //formgraphics.FillRectangle(floorBrush, // i sona
        //            //    new RectangleF(32, 32, 320, 32));

        //            //formgraphics.FillRectangle(floorBrush, // i apacia
        //            //    new RectangleF(320, 64, 32, 160));

        //            //formgraphics.FillRectangle(wallsBrush, // pirma i sona
        //            //    new RectangleF(0, 0, 352, 32));

        //            //formgraphics.FillRectangle(wallsBrush,
        //            //    new RectangleF(352, 0, 32, 256)); // pabaiga

        //            //formgraphics.FillRectangle(wallsBrush, // kairioji pradzia
        //            //    new RectangleF(0, 32, 32, 32));

        //            //formgraphics.FillRectangle(wallsBrush, // apacia i sona
        //            //    new RectangleF(0, 64, 320, 32));

        //            //formgraphics.FillRectangle(wallsBrush,
        //            //    new RectangleF(288, 96, 32, 160)); // i apacia

        //            //formgraphics.FillRectangle(wallsBrush,
        //            //    new RectangleF(320, 224, 32, 32)); // pabaiga

        //            //formgraphics.Dispose();

        //        }
        //        catch (System.IO.FileNotFoundException)
        //        {
        //            MessageBox.Show("there was an error opening the bitmap." +
        //                "please check the path.");
        //        }
        //    }
        //}
    }
}
