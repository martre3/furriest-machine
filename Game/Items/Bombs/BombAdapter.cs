using System;
using Maze.Game.Objects;

namespace Maze.Game.Items.Bombs
{
    [Serializable]
    public class BombAdapter: IItem
    {   
        private BombItem _bomb;

        public BombAdapter(BombItem bomb)
        {
            _bomb = bomb;
        }

        public void Use(Player player) 
        {
            _bomb.Plant(player);
        }

        public string GetName()
        {
            return _bomb.GetName();
        }
    }
}
