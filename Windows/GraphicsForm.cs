using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Maze.Windows
{
    public class GraphicsForm : Form
    {
        public bool Antialiasing { get; set; } = false;
        public BufferedGraphics BackBuffer { get; private set; }
        private Graphics _frontSurface;

        public GraphicsForm()
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            // Disable built in paint event. Handle re-paints manually.
            SetStyle(ControlStyles.UserPaint, false);

            SetStyle(ControlStyles.FixedHeight, true);
            SetStyle(ControlStyles.FixedWidth, true);
        }

        public void InitGraphicsBuffers()
        {
            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;

            _frontSurface = CreateGraphics();
            _frontSurface.CompositingMode = CompositingMode.SourceCopy;
            _frontSurface.CompositingQuality = CompositingQuality.AssumeLinear;
            _frontSurface.SmoothingMode = SmoothingMode.None;
            _frontSurface.InterpolationMode = InterpolationMode.NearestNeighbor;
            _frontSurface.TextRenderingHint = TextRenderingHint.SystemDefault;
            _frontSurface.PixelOffsetMode = PixelOffsetMode.HighSpeed;

            BackBuffer = currentContext.Allocate(_frontSurface, new Rectangle(0, 0, ClientSize.Width, ClientSize.Height));
            BackBuffer.Graphics.CompositingMode = CompositingMode.SourceOver;
            BackBuffer.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
            BackBuffer.Graphics.InterpolationMode = InterpolationMode.Low;
            BackBuffer.Graphics.PixelOffsetMode = PixelOffsetMode.Half;

            if (!Antialiasing)
            {
                return;
            }

            BackBuffer.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            BackBuffer.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        }
    }
}
