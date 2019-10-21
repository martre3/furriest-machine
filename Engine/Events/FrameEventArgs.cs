using System;
using System.Drawing;

namespace Maze.Engine.Events
{
    public class FrameEventArgs : EventArgs
    {
        public Graphics Surface { get; private set; }

        public FrameEventArgs(Graphics surface)
        {
            Surface = surface;
        }
    }
}
