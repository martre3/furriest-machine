using System;
using System.Drawing;
using Maze.Engine.Events;
using Maze.Windows;

namespace Maze.Engine.Renderer
{
    public class GDIRenderer : IRenderer
    {
        public event EventHandler<FrameEventArgs> OnFrame;

        private Graphics _surface;
        private BufferedGraphics _graphicsBuffer;
        private GraphicsForm _graphicsForm;

        public GDIRenderer(GraphicsForm graphicsForm)
        {
            _graphicsForm = graphicsForm;
        }

        public void Initialize()
        {
            _graphicsForm.InitGraphicsBuffers();
            _graphicsBuffer = _graphicsForm.BackBuffer;
            _surface = _graphicsBuffer.Graphics;
        }

        public void Frame()
        {
            _surface.Clear(Color.Purple);

            OnFrame.Raise(this, new FrameEventArgs(_surface));
        }

        public void Render()
        {
            _graphicsBuffer.Render();
        }
    }
}
