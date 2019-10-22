using System;
using Maze.Engine.Events;

namespace Maze.Engine.Renderer
{
    public interface IRenderer
    {
        void Initialize();
        void Frame();
        void Render();
        event EventHandler<FrameEventArgs> OnFrame;
    }
}
