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

        [NonSerialized]
        private Brush _brush;

        public override void InitializeAssets(AssetsLoader assetsLoader)
        {
            boundingBox = new Rectangle(pos, size);

            _brush = assetsLoader.LoadBrush(this.TextureFile);
        }

        public override void Draw(Graphics surface)
        {
            // boundingBox.X = pos.X;
            // boundingBox.Y = pos.Y;

            if (_brush == null) {
                System.Diagnostics.Debug.WriteLine("false");
            }
            
            if (_brush != null) {
                surface.FillRectangle(_brush, boundingBox);
            }
        }

        public override void Update(IQueryableFormInput input, UpdateEventArgs e)
        {

        }
    }
}
