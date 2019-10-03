using System;
using System.Drawing;
using Maze.Engine.Input;
using Maze.Engine.Events;
using Maze.Game.Assets;

namespace Maze.Game.Objects
{
    [Serializable]
    public abstract class GameObject
    {
        public int? Id;
        public int clientHeight;
        public int clientWidth;

        public GameObject()
        {
            // TODO: Have to make these, and any other relevant state data readily available for all GameObject(s).
            clientWidth = 800;
            clientHeight = 576;
        }

        public Point pos;
        public Rectangle boundingBox;
        public Size size;
        public virtual void Initialize() { }
        public virtual void InitializeAssets(AssetsLoader assetsLoader) {
        }
        public abstract void Update(IQueryableFormInput input, UpdateEventArgs e);
        public abstract void Draw(Graphics surface);

        public virtual void Sync(GameObject gameObject)
        {
            this.pos = gameObject.pos;
            this.size = gameObject.size;
        }
    }
}
