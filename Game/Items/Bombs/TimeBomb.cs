using System;
using Maze.Game.Objects;

namespace Maze.Game.Items.Bombs
{
    [Serializable]
    public class TimeBomb: BombItem
    {   
        public override void Plant(Player player)
        {
            if (!(player.state is ClientGameState))
            {
                player.BombMediator.Register(new Bomb(player, new RegressBuff(), player.BombMediator) {
                    Position = new System.Drawing.Point(player.Position.X, player.Position.Y),
                });
            }
        }
        
        public override string GetName()
        {
            return "Time Bomb";
        }
    }
}
