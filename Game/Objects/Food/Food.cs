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

namespace Maze.Game.Objects.Food
{
    [Serializable]
    abstract public class Food: GameObject
    {
        public Food(Point position)
        {
            _mesh.Position = position;
            _mesh.IsTrigger = true;
            size = new Size(32, 32);
        }

        public override void InitializeAssets(AssetsLoader assetsLoader)
        {
            boundingBox = new Rectangle(_mesh.Position, size);
            _brush = (TextureBrush) assetsLoader.LoadBrush("food.png");
        }

        public override void Draw(Graphics surface)
        {
            if (_brush == null)
            {
                return;
            }

            boundingBox.X = _mesh.Position.X;
            boundingBox.Y = _mesh.Position.Y;

            _brush.ResetTransform();
            _brush.TranslateTransform(_mesh.Position.X, _mesh.Position.Y);
            surface.FillRectangle(_brush, boundingBox);
        }

        public override void Update(IQueryableFormInput input, UpdateEventArgs e)
        {
            
        }

        abstract public void PickUp();
    }
}
