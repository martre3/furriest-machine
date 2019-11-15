using Maze.Game.Objects;

namespace Maze.Game.Items
{
    public interface IItem
    {
        void Use(Player player);
        string GetName();
    }
}
