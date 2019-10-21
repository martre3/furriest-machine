using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Maze.Engine.Events;
using Maze.Engine.Input;


namespace Maze.Game.Objects
{
    class Example: GameObject
    {
        Point pos;
        Point direction;
        double velocity;
        Size size;

        Rectangle boundingBox;

        int clientHeight;
        int clientWidth;

        Random rng;

        public Example()
        {
            rng = new Random();

            pos = new Point(0, 0);
            velocity = 10;
            size = new Size(10, 10);
            direction = new Point(1, 1);

            // TODO: Have to make these, and any other relevant state data readily available for all GameObject(s).
            clientWidth = 800;
            clientHeight = 576;
        }

        public override void InitializeAssets()
        {
            // Not really an asset, but any image loading, graphics pre-generation would go here.
            boundingBox = new Rectangle(pos, size);
        }

        public override void Draw(Graphics surface)
        {
            boundingBox.X = pos.X;
            boundingBox.Y = pos.Y;

            surface.FillEllipse(Brushes.White, boundingBox);
        }

        public override void Update(IQueryableFormInput input, UpdateEventArgs e)
        {
            pos.X += (int)(velocity * direction.X);
            pos.Y += (int)(velocity * direction.Y);

            if (input.IsKeyDown(Keys.Down))
            {
                direction.Y = 1;
            }
            else if (input.IsKeyDown(Keys.Up))
            {
                direction.Y = -1;
            }
            else if (input.IsKeyDown(Keys.Right))
            {
                direction.X = 1;
            }
            else if (input.IsKeyDown(Keys.Left))
            {
                direction.X = -1;
            }

            if (pos.X + size.Width >= clientWidth || pos.X <= 0)
            {
                direction.X = -direction.X;
            }
            else if (pos.Y + size.Height >= clientHeight || pos.Y <= 0)
            {
                direction.Y = -direction.Y;
            }
        }
    }
}
