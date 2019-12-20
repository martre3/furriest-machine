using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Net.Sockets;
using System.Threading;
using Maze.Game.Items;
using System.Drawing;
using System.Windows.Forms;
using Maze.Engine.Events;
using Maze.Engine.Input;
using Maze.Game.Assets;
using Maze.Engine.Physics;
using Maze.Game.Objects.PickUp;
using Maze.Game.Mediators;

namespace Maze.Game.Objects
{
    [Serializable]
    public class Explosion: GameObject
    {
        private double _lifetime = 500;

        public Explosion(Point pos)
        {
            this.Position = pos;
            _mesh.Size = new Size(32, 32);
            this._mesh.IsTrigger = true;
        }

        public override void InitializeAssets(AssetsLoader assetsLoader)
        {
            boundingBox = new Rectangle(this.Position, size);
            _brush = (TextureBrush) assetsLoader.LoadBrush("explosion.png");
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
            this._lifetime -= e.UpdateTimeStep;

            if (this._lifetime <= 0)
            {
                this.Destroy();
            }
        }
    }
}
