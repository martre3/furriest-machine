using System;
using System.Drawing;
using Maze.Engine.Input;
using Maze.Engine.Events;
using Maze.Game.Assets;
using Maze.Game.Commands;
using Maze.Engine.Physics;

namespace Maze.Game.Objects
{
    [Serializable]
    public abstract class GameObject: ICollidable
    {
        public GameState state;

        public int? Id;
        public int clientHeight;
        public int clientWidth;

        protected CommandQueue commands;
        protected virtual bool UsesCommands { get; } = false;

        protected Mesh _mesh;

        [NonSerialized]
        protected TextureBrush _brush;

        public bool IsDestroying { get; protected set; } = false;

        public bool IsDestroyed { get; protected set; } = false;

        public GameObject()
        {
            _mesh = new Mesh();
            _mesh.Object = this;

            // TODO: Have to make these, and any other relevant state data readily available for all GameObject(s).
            clientWidth = 800;
            clientHeight = 576;

            if (UsesCommands)
            {
                commands = new CommandQueue();
            }
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
        public virtual void Update(IQueryableFormInput input, UpdateEventArgs e) {}
        public virtual void Update(IQueryableFormInput input, UpdateEventArgs e, GameState currentState)
        {
            this.state = currentState;
            Update(input, e);
        }

        public abstract void Draw(Graphics surface);

        protected virtual void OnDestroy() {}

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

        public void Destroy()
        {
            IsDestroying = true;
            this._mesh.IsVisible = false;
            this._mesh.IsCollider = false;
            OnDestroy();
            IsDestroyed = true;
        }
    }
}
