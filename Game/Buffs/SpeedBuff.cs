using System;
using Maze.Game.Objects;

namespace Maze.Game.Items
{
    [Serializable]
    public class SpeedBuff: IBuff
    {   
        private float _multiplier;
        private float _previousMultiplier;

        public SpeedBuff(float multiplier)
        {
            _multiplier = multiplier;
        }

        public void Apply(Player player)
        {
            _previousMultiplier = player.SpeedMultiplier;
            player.SpeedMultiplier = _multiplier;
        }

        public void Undo(Player player)
        {
            player.SpeedMultiplier = _previousMultiplier;
        }
    }
}
