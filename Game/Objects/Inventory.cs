using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using Maze.Engine.Input;
using Maze.Engine.Events;
using System;
using System.Collections.Generic;
using Maze.Game.Items;

namespace Maze.Game.Objects
{
    [Serializable]
    public class Inventory: GameObject
    {
        public Dictionary<int, IItem> _items = new Dictionary<int, IItem>();
        private Font _font = new Font("Courier New", 10);

        public Inventory()
        {
            
        }

        public bool Put(IItem item)
        {
            for (int i = 0; i < 10; i++) {
                if (!_items.ContainsKey(i))
                {
                    _items.Add(i, item);
                    
                    return true;
                }
            }

            return false;
        }

        public override void Sync(GameObject gameObject)
        {
            base.Sync(gameObject);

            _items = ((Inventory) gameObject)._items;
        }

        public override void Draw(Graphics surface)
        {
            for (int i = 1; i <= 10; i++) {
                var name = _items.ContainsKey(i % 10) ? _items[i % 10].GetName() : "Empty";
                surface.DrawString(String.Format("{0}: {1}", i % 10, name), _font, Brushes.White, 500, i * 32);
            }
        }
    }
}
