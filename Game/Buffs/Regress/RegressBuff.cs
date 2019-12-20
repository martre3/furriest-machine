using System;
using Maze.Game.Objects;

namespace Maze.Game.Items
{
    [Serializable]
    public class RegressBuff: IBuff
    {   
        private RegressMemento _memento;

        public void Apply(Player player)
        {
            _memento = player.GetMemento();
        }

        public void Undo(Player player)
        {
            player.SetMemento(_memento);
        }
    }
}
