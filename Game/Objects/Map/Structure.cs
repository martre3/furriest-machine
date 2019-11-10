using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Engine.Events;
using Maze.Engine.Input;
using System.Drawing;
using Maze.Game.Assets;

namespace Maze.Game.Objects.Map
{
    [Serializable]
    abstract public class Structure: GameObject
    {
        protected string TextureFile;

        public override void InitializeAssets(AssetsLoader assetsLoader)
        {
            boundingBox = new Rectangle(this.Position, size);

            _brush = (TextureBrush) assetsLoader.LoadBrush(this.TextureFile);
        }

        public override void Draw(Graphics surface)
        {
            // boundingBox.X = pos.X;
            // boundingBox.Y = pos.Y;
            
            if (_brush != null) {
                surface.FillRectangle(_brush, boundingBox);
            }
        }

        public override void Update(IQueryableFormInput input, UpdateEventArgs e)
        {

        }
    }
}
