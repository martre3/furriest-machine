using System;
using Maze.Game.Objects;

namespace Maze.Game.Items.Bombs
{
    public interface IBomb
    {   
        void Plant(Player player);
        string GetName();
    }
}
