using System;
using Maze.Game.Objects;

namespace Maze.Game.Items.Bombs
{
    [Serializable]
    public class FreezeBomb: IBomb
    {   
        public void Plant(Player player)
        {
            if (!(player.state is ClientGameState))
            {
                player.state.Register(new Bomb(player, new SpeedBuff(0)) {
                    Position = new System.Drawing.Point(player.Position.X, player.Position.Y),
                });
            }
        }

        public string GetName()
        {
            return "Freeze Bomb";
        }
    }
}
