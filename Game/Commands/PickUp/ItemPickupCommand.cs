using Maze.Game.Objects;
using Maze.Game.Objects.PickUp;
using System;

namespace Maze.Game.Commands
{
    [Serializable]
    public class ItemPickupCommand: PickupCommand<Food, Player>
    {
        public ItemPickupCommand(Food source, Player target) : base(source, target)
        {
        }

        protected override void Apply()
        {
            this.source.PickUp(this.target);
        }
    }
}
