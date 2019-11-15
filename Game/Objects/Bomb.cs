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

namespace Maze.Game.Objects
{
    [Serializable]
    public class Bomb: GameObject
    {
        private Player _owner;
        private IBuff _buff;

        public Bomb(Player owner, IBuff buff)
        {
            _owner = owner;
            _buff = buff;
            _mesh.IsTrigger = true;
            _mesh.Size = new Size(16, 16);
        }

        // public override void OnCollision(Collision collision)
        // {
        //     if (collision.CollidedWith is Food)
        //     {
        //         ((Food) collision.CollidedWith).PickUp(this);
        //     }
        // }

        public override void InitializeAssets(AssetsLoader assetsLoader)
        {
            boundingBox = new Rectangle(this.Position, size);
            _brush = (TextureBrush) assetsLoader.LoadBrush("bomb.png");
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
            
        }

        public override bool IsDynamic()
        {
            return true;
        }
    }
}
