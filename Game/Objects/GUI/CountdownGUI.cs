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
    public class CountDownGUI: GameObject
    {
        public double CurrentTime;
        private Font _font = new Font("Courier New", 90);

        [NonSerialized]
        private Brush _brush;

        public override void Sync(GameObject gameObject)
        {
            base.Sync(gameObject);

            var newTime = ((CountDownGUI) gameObject).CurrentTime;

            if (CurrentTime != newTime)
            {
                this.CurrentTime = newTime;
            }
        }

        public override void Draw(Graphics surface)
        {
            if (_brush == null)
            {
                _brush = Brushes.Yellow;
            }

            surface.DrawString(Math.Round(this.CurrentTime).ToString(), _font, _brush, 350, 200);
        }
    }
}
