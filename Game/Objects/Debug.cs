using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
// TODO: Will get annoying pulling this in only for "Keys". Further investigation needed.
using System.Windows.Forms;

using Maze.Engine.Input;
using Maze.Engine.Events;
using System;

namespace Maze.Game.Objects
{
    public class Diagnostics: GameObject
    {
        private double _frameRate;
        private double _frameTime;

        Font font;

        public Diagnostics()
        {
            font = new Font("Courier New", 10);
        }

        public override void Draw(Graphics surface)
        {
            var fpsStr = $"FPS: {_frameRate:0.00}";
            var frameTimeStr = $"{_frameTime:0.00} ms";
            var fpsStrSize = surface.MeasureString(fpsStr, font);
            var frameTimeStrSize = surface.MeasureString(frameTimeStr, font);
            var len = Math.Max(fpsStrSize.Width, frameTimeStrSize.Width);

            surface.FillRectangle(Brushes.Black, 0, 0, len, fpsStrSize.Height);
            surface.DrawString(fpsStr, font, Brushes.White, 0, 0);

            surface.FillRectangle(Brushes.Black, 0, fpsStrSize.Height, len, fpsStrSize.Height);
            surface.DrawString(frameTimeStr, font, Brushes.White, 0, fpsStrSize.Height);
        }

        public override void Update(IQueryableFormInput input, UpdateEventArgs e)
        {
            _frameRate = e.FrameRate;
            _frameTime = e.LastFrameTime;
        }
    }
}
