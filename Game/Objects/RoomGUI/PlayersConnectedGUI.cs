using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using Maze.Engine.Input;
using Maze.Engine.Events;
using System;
using System.Collections.Generic;
using Maze.Game.Items;

namespace Maze.Game.Objects.RoomGUI
{
    [Serializable]
    public class PlayersConnectedGUI: GameObject
    {
        readonly int MaxPlayerCount = 5;

        public List<String> PlayerNames = new List<String>();

        private Font _playersConnectedFont = new Font("Courier New", 15);
        private Font _font = new Font("Courier New", 10);

        public override void Sync(GameObject gameObject)
        {
            this.PlayerNames = ((PlayersConnectedGUI) gameObject).PlayerNames;
        }

        public override void Draw(Graphics surface)
        {
            var current = 0;

            surface.DrawString(String.Format("{0} / {1} Players connected", this.PlayerNames.Count, this.MaxPlayerCount), _playersConnectedFont, Brushes.White, 500, 32);

            PlayerNames.ForEach(name => {
                surface.DrawString(String.Format("{0}", name), _font, Brushes.White, 600, current * 70);
                current++;
            });
        }
    }
}
