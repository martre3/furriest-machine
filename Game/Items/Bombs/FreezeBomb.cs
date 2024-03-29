﻿using System;
using Maze.Game.Objects;
using Maze.Game.Mediators;

namespace Maze.Game.Items.Bombs
{
    [Serializable]
    public class FreezeBomb: BombItem
    {   
        public override void Plant(Player player)
        {
            if (!(player.state is ClientGameState))
            {
                player.BombMediator.Register(new Bomb(player, new SpeedBuff(0), player.BombMediator) {
                    Position = new System.Drawing.Point(player.Position.X, player.Position.Y),
                });
            }
        }

        public override string GetName()
        {
            return "Freeze Bomb";
        }
    }
}
