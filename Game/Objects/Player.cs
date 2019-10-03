using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Net.Sockets;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using Maze.Engine.Events;
using Maze.Engine.Input;
using Maze.Game.Assets;

namespace Maze.Game.Objects
{
    [Serializable]
    public class Player: GameObject
    {
        public int UserId { get; set; }
        Point direction;
        double velocity;

        [NonSerialized]
        Random rng;

        [NonSerialized]
        private TextureBrush _brush;

        public Player()
        {
            rng = new Random();

            pos = new Point(0, 0);
            velocity = 10;
            size = new Size(10, 10);
            direction = new Point(1, 1);
        }

        public override void InitializeAssets(AssetsLoader assetsLoader)
        {
            boundingBox = new Rectangle(pos, size);
            _brush = (TextureBrush) assetsLoader.LoadBrush("slime.png");
        }

        public override void Draw(Graphics surface)
        {
            boundingBox.X = pos.X;
            boundingBox.Y = pos.Y;

            _brush.ResetTransform();
            _brush.TranslateTransform(pos.X, pos.Y);
            surface.FillRectangle(_brush, boundingBox);
        }

        public override void Update(IQueryableFormInput input, UpdateEventArgs e)
        {
            if (input.IsUserKeyDown(UserId, Keys.W)) {
                pos.Y -= 1;
            }
            if (input.IsUserKeyDown(UserId, Keys.D)) {
                pos.X += 1;
            }
            if (input.IsUserKeyDown(UserId, Keys.S)) {
                pos.Y += 1;
            }
            if (input.IsUserKeyDown(UserId, Keys.A)) {
                pos.X -= 1;
            }
        }

        public override void Sync(GameObject gameObject)
        {
            base.Sync(gameObject);

            var player = (Player) gameObject;

            this.direction = player.direction;
            this.velocity = player.velocity;
        }
    }
}
