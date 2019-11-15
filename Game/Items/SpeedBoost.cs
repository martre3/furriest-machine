using System;
using Maze.Game.Objects;

namespace Maze.Game.Items
{
    [Serializable]
    public class SpeedBoost: IItem
    {   
        private float _multiplier;

        public SpeedBoost(float multiplier)
        {
            _multiplier = multiplier;
        }

        public void Use(Player player)
        {
            player.Buff(new SpeedBuff(_multiplier));
        }
        
        public string GetName()
        {
            return "Speed Boost";
        }
    }
}
