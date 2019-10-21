using System.Drawing;
using Maze.Engine.Input;
using Maze.Engine.Events;

namespace Maze.Game.Objects
{
    public abstract class GameObject
    {
        public virtual void Initialize() { }
        public virtual void InitializeAssets() { }
        public abstract void Update(IQueryableFormInput input, UpdateEventArgs e);
        public abstract void Draw(Graphics surface);
    }
}
