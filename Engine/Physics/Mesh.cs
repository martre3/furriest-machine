using System;
using System.Drawing;

namespace Maze.Engine.Physics
{
    [Serializable]
    public class Mesh
    {
        public int ID;
        public Point Position;
        public Size Size;
        public bool IsVisible = true;
        public bool SmoothSync = true;
        public bool IsCollider = true;
        public bool IsTrigger = false;

        public object Object;

        private Point Direction = new Point(0, 0);
        public Point RealPosition;

        public Mesh()
        {
            this.Position = new Point(0, 0);
            this.Direction = new Point(0, 0);
            this.Size = new Size(32, 32);
        }

        public void Translate(int x, int y)
        {
            this.Direction.X += x;
            this.Direction.Y += y;
        }

        public Point GetDirection()
        {
            return this.Direction;
        }

        public void ResetDirection()
        {
            this.Direction.X = 0;
            this.Direction.Y = 0;
        }

        public bool IsColliding(Mesh other)
        {
            return (this.IsPointInSegment(this.GetFutureX(), other.GetFutureX(), other.Size.Width) && 
                this.IsPointInSegment(this.GetFutureY(), other.GetFutureY(), other.Size.Height)) || 
                (this.IsPointInSegment(this.GetFutureX(), other.GetFutureX(), other.Size.Width) &&
                 this.IsPointInSegment(this.GetFutureY() + Size.Height, other.GetFutureY(), other.Size.Height)) || 
                (this.IsPointInSegment(this.GetFutureX() + Size.Height, other.GetFutureX(), other.Size.Width) && 
                this.IsPointInSegment(this.GetFutureY(), other.GetFutureY(), other.Size.Height)) ||
                (this.IsPointInSegment(this.GetFutureX() + Size.Height, other.GetFutureX(), other.Size.Width) && 
                this.IsPointInSegment(this.GetFutureY() + Size.Height, other.GetFutureY(), other.Size.Height));
        }

        private int GetFutureX()
        {
            return this.Position.X + this.Direction.X;
        }

        private int GetFutureY()
        {
            return this.Position.Y + this.Direction.Y;
        }

        private bool IsPointInSegment(int point, int segmentStart, int segmentLenght)
        {
            return point <= segmentStart + segmentLenght && point >= segmentStart;
        }
    }
}
