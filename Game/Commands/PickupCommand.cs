using Maze.Game.Objects;
using Maze.Game.Objects.PickUp;
using System;

namespace Maze.Game.Commands
{
    [Serializable]
    public abstract class PickupCommand<S, T>: ICommand
        where S: Food
        where T: GameObject
    {
        protected S source;
        protected T target;

        public PickupCommand(S source, T target)
        {
            this.source = source;
            this.target = target;
        }

        public void Execute()
        {
            Apply();
            source.Destroy();
        }

        protected abstract void Apply();
    }
}
