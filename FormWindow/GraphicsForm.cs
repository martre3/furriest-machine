using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win32;
using Maze.src.engine;
using System.Threading;
using System.Collections.Generic;

namespace FormWindow
{
    public partial class GraphicsForm : Form
    {
        private readonly SemaphoreSlim BufferLock = new SemaphoreSlim(1, 1);
        public static event EventHandler<EventArgs> Rendering;
        public static event EventHandler<KeyEventArgs> KeyPressed;

        private bool antialiasing = false;
        private Size displaySize;

        private Stopwatch sw;

        private Graphics G_TARGET;

        private BufferedGraphicsContext currentContext;

        private BufferedGraphics G_BUFFER;
        private Random rng;
        double elapsed = 0;
        private Font font;
        private Brush hBrush;
        private Keys key;

        private void InitGraphicsBuffers(Size region)
        {
            currentContext = BufferedGraphicsManager.Current;

            // Front buffer
            G_TARGET = CreateGraphics();
            G_TARGET.CompositingMode = CompositingMode.SourceCopy;
            G_TARGET.CompositingQuality = CompositingQuality.AssumeLinear;
            G_TARGET.SmoothingMode = SmoothingMode.None;
            G_TARGET.InterpolationMode = InterpolationMode.NearestNeighbor;
            G_TARGET.TextRenderingHint = TextRenderingHint.SystemDefault;
            G_TARGET.PixelOffsetMode = PixelOffsetMode.HighSpeed;

            // Back buffer. All draw calls go here.
            G_BUFFER = currentContext.Allocate(G_TARGET, new Rectangle(0, 0, region.Width, region.Height));
            G_BUFFER.Graphics.CompositingMode = CompositingMode.SourceOver;
            G_BUFFER.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
            G_BUFFER.Graphics.InterpolationMode = InterpolationMode.Low;
            G_BUFFER.Graphics.PixelOffsetMode = PixelOffsetMode.Half;

            if (!antialiasing)
            {
                return;
            }

            G_BUFFER.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            G_BUFFER.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        }

        public GraphicsForm()
        {
            InitializeComponent();

            font = new Font("Courier New", 10);
            hBrush = Brushes.White;

            displaySize = new Size(1280, 720);

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, false);

            ClientSize = displaySize;

            FormBorderStyle = FormBorderStyle.FixedSingle;

            SetStyle(ControlStyles.FixedHeight, true);
            SetStyle(ControlStyles.FixedWidth, true);

            InitGraphicsBuffers(displaySize);

            sw = new Stopwatch();

            rng = new Random();

            Task.Run(RenderLoop);

            new Game(this);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            key = e.KeyCode;
            GraphicsForm.KeyPressed?.Invoke(this, e);
        }

        public new void Paint(List<(Brush, Rectangle)> pairs)
        {
            this.BufferLock.Wait();
            try
            {
                G_BUFFER.Graphics.Clear(Color.Black);
                G_BUFFER.Graphics.DrawString("Text", DefaultFont, new SolidBrush(Color.White), 0, 0);
                
                pairs.ForEach(pair => G_BUFFER.Graphics.FillRectangle(pair.Item1, pair.Item2));

                if (elapsed > 0)
                {
                    var sSize = G_BUFFER.Graphics.MeasureString($"FPS: {(1000f / elapsed):0}", font);
                    var sSize2 = G_BUFFER.Graphics.MeasureString($"FPS: {elapsed:0.00}", font);
                    G_BUFFER.Graphics.FillRectangle(Brushes.Black, 0, 0, sSize2.Width, sSize.Height);
                    G_BUFFER.Graphics.DrawString($"FPS: {(1000f / elapsed):0}", font, hBrush, 0, 0);
                    G_BUFFER.Graphics.FillRectangle(Brushes.Black, 0, sSize.Height, sSize2.Width, sSize2.Height);
                    G_BUFFER.Graphics.DrawString($"{elapsed:0.00} ms", font, hBrush, 0, sSize.Height);

                    if (key != Keys.None)
                    {
                        G_BUFFER.Graphics.FillRectangle(Brushes.Black, 0, sSize.Height * 2, sSize2.Width, sSize2.Height);
                        G_BUFFER.Graphics.DrawString($"KEY: {key}", font, hBrush, 0, sSize.Height * 2);
                    }
                }
            }
            finally
            {
                this.BufferLock.Release();
            }
        }

        private void RenderLoop()
        {
            sw.Restart();
//            double internalLapse;

            while (IsApplicationIdle())
            {
                if (!Disposing && !IsDisposed && Visible)
                {
//                    internalLapse = sw.Elapsed.TotalMilliseconds;
//                    if (internalLapse < 31)
//                    {
//                        Debug.WriteLine("sleep");
//                        Thread.Sleep(31 - (int)internalLapse);
//                    }

                    sw.Stop();
                    elapsed = sw.Elapsed.TotalMilliseconds;

                    sw.Restart();

                    GraphicsForm.Rendering?.Invoke(this, EventArgs.Empty);

                    // Should pass G_BUFFER.Graphics
                    this.BufferLock.Wait();
                    try
                    {
                        G_BUFFER.Render();
                    }
                    finally
                    {
                     this.BufferLock.Release();
                    }

                    G_BUFFER.Render();
                }
            }
        }

        private bool IsApplicationIdle()
        {
            return !Win32Native.PeekMessage(out _, IntPtr.Zero, (uint) 0, (uint) 0, (uint) 0);
        }

    }
}
