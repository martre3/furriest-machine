using System;
using Maze.Game.Items;

namespace Maze.Game.Objects.PickUp
{
    [Serializable]
    public class ItemPickup: Food
    {
        private IItem item { get; }

        public ItemPickup(IItem item): base() {
            this.item = item;
        }
        
        public override void PickUp(Player player) 
        {
            if (player.Inventory.Put(this.item))
            {
                this.Destroy();
            }
        }
    }
}
