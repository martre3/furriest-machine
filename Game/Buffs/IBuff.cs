using Maze.Game.Objects;

namespace Maze.Game.Items
{
    public interface IBuff
    {
        void Apply(Player player);
        void Undo(Player player);
    }
}
