using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using Maze.Engine.Input;
using Maze.Engine.Events;
using System;
using System.Collections.Generic;
using Maze.Game.Items;
using Maze.Game.Enums;

namespace Maze.Game.Objects.GUI
{
    [Serializable]
    public class EndGameGUI: GameObject
    {
        public List<int> UsersWon;

        private Font _font = new Font("Courier New", 50);

        public override void Sync(GameObject gameObject)
        {
            this.UsersWon = ((EndGameGUI) gameObject).UsersWon;
        }

        public override void Draw(Graphics surface)
        {
            if (this.UsersWon.Contains(this.state.UserId))
            {
                surface.DrawString("VICTORY", _font, Brushes.Green, 250, 200);
            }
            else 
            {
                surface.DrawString("DEFEAT", _font, Brushes.Red, 270, 200);
            }
        }
    }
}
