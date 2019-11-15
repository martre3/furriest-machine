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
using Maze.Engine.Physics;
using Maze.Game.Assets;
using Maze.Game.Commands;
using Maze.Game.Objects.Food;

namespace Maze.Game.Objects
{
    [Serializable]
    public class Player: GameObject
    {
        public int UserId { get; set; }
        // TODO: Should be float
        public int SpeedMultiplier { get; set; } = 1;
        protected override bool UsesCommands { get; } = true;

        public Player(Point position): base()
        {
            this.Position = position;
            this.size = new Size(19, 12);
        }

        public override void InitializeAssets(AssetsLoader assetsLoader)
        {
            boundingBox = new Rectangle(this.Position, size);
            _brush = (TextureBrush) assetsLoader.LoadBrush("slime.png");
        }

        public override void Draw(Graphics surface)
        {
            if (_brush == null)
            {
                return;
            }

            boundingBox.X = this.Position.X;
            boundingBox.Y = this.Position.Y;

            _brush.ResetTransform();
            _brush.TranslateTransform(this.Position.X, this.Position.Y);
            surface.FillRectangle(_brush, boundingBox);
        }

        public override void Update(IQueryableFormInput input, UpdateEventArgs e)
        {
            if (input.IsUserKeyDown(UserId, Keys.W)) {
                _mesh.Translate(0, -1 * SpeedMultiplier);
            }
            if (input.IsUserKeyDown(UserId, Keys.D)) {
                _mesh.Translate(1 * SpeedMultiplier, 0);
            }
            if (input.IsUserKeyDown(UserId, Keys.S)) {
                _mesh.Translate(0, 1 * SpeedMultiplier);
            }
            if (input.IsUserKeyDown(UserId, Keys.A)) {
                _mesh.Translate(-1 * SpeedMultiplier, 0);
            }

            if (!commands.IsEmpty())
            {
                commands.Dequeue().Execute();
            }
        }

        public override bool IsDynamic()
        {
            return true;
        }

        public override void OnCollision(Collision collision)
        {
            if (collision.CollidedWith is BombPickup)
            {
                commands.Enqueue(new BombPickupCommand((BombPickup) collision.CollidedWith, this));
            }
        }
    }
}
