using System;
using Maze.Game.Objects;

namespace Maze.Game.Items.Bombs
{
    [Serializable]
    public class SlowBomb: IBomb
    {   
        public void Plant(Player player)
        {
            
        }
        
        public string GetName()
        {
            return "Slow Bomb";
        }
    }
}
