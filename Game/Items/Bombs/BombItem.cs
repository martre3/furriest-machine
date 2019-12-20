using System;
using Maze.Game.Objects;
using Maze.Game.Mediators;

namespace Maze.Game.Items.Bombs
{
    [Serializable]
    abstract public class BombItem
    {   
        [NonSerialized]
        public IBombMediator Mediator;

        abstract public void Plant(Player player);
        abstract public string GetName();
    }
}
