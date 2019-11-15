using Maze.Game.Objects;
using Maze.Game.Objects.Food;

namespace Maze.Game.Commands
{
    public class BombPickupCommand: PickupCommand<Food, Player>
    {
        public BombPickupCommand(Food source, Player target) : base(source, target)
        {
        }

        protected override void Apply()
        {
            target.SpeedMultiplier = 2;
        }
    }
}
