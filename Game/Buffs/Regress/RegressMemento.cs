using System;
using Maze.Game.Objects;
using System.Drawing;

namespace Maze.Game.Items
{
    [Serializable]
    public class RegressMemento
    {   
        private Point _position;

        public RegressMemento(Point position)
        {
            _position = position;
        }

        public Point GetPoisition()
        {
            return this._position;
        }
    }
}
