using System;
using System.Drawing;
using Maze.Engine.Input;
using Maze.Engine.Events;
using Maze.Game.Assets;
using Maze.Engine.Physics;

namespace Maze.Game.Objects
{
    [Serializable]
    public abstract class GameObject: ICollidable
    {
        public int? Id;
        public int clientHeight;
        public int clientWidth;

        protected Mesh _mesh;

        [NonSerialized]
        protected TextureBrush _brush;

        public GameObject()
        {
            _mesh = new Mesh();

            // TODO: Have to make these, and any other relevant state data readily available for all GameObject(s).
            clientWidth = 800;
            clientHeight = 576;
        }

        public Point Position { 
            get {
                return this._mesh.Position;
            }
            set {
               _mesh.Position = value; 
            }
        }

        public Size size { 
            get {
                return _mesh.Size;
            }
            set {
               _mesh.Size = value; 
            }
        }

        public Rectangle boundingBox;
        
        public virtual void Initialize() { }
        public virtual void InitializeAssets(AssetsLoader assetsLoader) {
        }
        public virtual void OnCollision(Collision collision) {}
        public abstract void Update(IQueryableFormInput input, UpdateEventArgs e);
        public abstract void Draw(Graphics surface);

        public virtual void Sync(GameObject gameObject)
        {
            this.Position = gameObject.Position;
            this.size = gameObject.size;
        }

        public virtual bool IsDynamic()
        {
            return false;
        }

        public Mesh GetMesh()
        {
            return this._mesh;
        }
    }
}
