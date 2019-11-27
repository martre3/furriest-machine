using System.Drawing;

namespace Maze.Windows
{
    public interface IGraphicsForm
    {
        bool Antialiasing { get; set; }
        BufferedGraphics BackBuffer { get; }
        void InitGraphicsBuffers();
    }
}
