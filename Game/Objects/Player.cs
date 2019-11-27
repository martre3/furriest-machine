using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Net.Sockets;
using System.Threading;
using Maze.Game.Items;
using System.Drawing;
using System.Windows.Forms;
using Maze.Engine.Events;
using Maze.Engine.Input;
using Maze.Engine.Physics;
using Maze.Game.Assets;
using Maze.Game.Commands;
using Maze.Game.Objects.PickUp;

namespace Maze.Game.Objects
{
    [Serializable]
    public class Player: GameObject
    {
        public int UserId { get; set; }
        // TODO: Should be float
        public float SpeedMultiplier { get; set; } = 1;
        protected override bool UsesCommands { get; } = true;
        public Inventory Inventory { get; }
        public List<IBuff> Buffs = new List<IBuff>();

        private Dictionary<Keys, int> _keysMap = new Dictionary<Keys, int>();
        private IBuff _activeBuff;

        public Player(Point position, Inventory inventory): base()
        {
            this.Position = position;
            this.Inventory = inventory;
            this.size = new Size(19, 12);
            _keysMap[Keys.NumPad0] = 0;
            _keysMap[Keys.NumPad1] = 1;
            _keysMap[Keys.NumPad2] = 2;
            _keysMap[Keys.NumPad3] = 3;
            _keysMap[Keys.NumPad4] = 4;
            _keysMap[Keys.NumPad5] = 5;
            _keysMap[Keys.NumPad6] = 6;
            _keysMap[Keys.NumPad7] = 7;
            _keysMap[Keys.NumPad8] = 8;
            _keysMap[Keys.NumPad9] = 9;

            
        }

        public override void OnCollision(Collision collision)
        {
            if (collision.CollidedWith is Food)
            {
                commands.Enqueue(new ItemPickupCommand((Food) collision.CollidedWith, this));
                // ((Food) collision.CollidedWith).PickUp(this);
            }
        }

        public override void InitializeAssets(AssetsLoader assetsLoader)
        {
            boundingBox = new Rectangle(this.Position, size);
            _brush = (TextureBrush) assetsLoader.LoadBrush("slime.png");
        }

        public override void Draw(Graphics surface)
        {
            if (_brush == null)
            {
                return;
            }

            boundingBox.X = this.Position.X;
            boundingBox.Y = this.Position.Y;

            _brush.ResetTransform();
            _brush.TranslateTransform(this.Position.X, this.Position.Y);
            surface.FillRectangle(_brush, boundingBox);
        }

        public override void Update(IQueryableFormInput input, UpdateEventArgs e)
        {
            if (input.IsUserKeyDown(UserId, Keys.W)) {
                _mesh.Translate(0, -1 * (int) SpeedMultiplier);
            }
            if (input.IsUserKeyDown(UserId, Keys.D)) {
                _mesh.Translate(1 * (int) SpeedMultiplier, 0);
            }
            if (input.IsUserKeyDown(UserId, Keys.S)) {
                _mesh.Translate(0, 1 * (int) SpeedMultiplier);
            }
            if (input.IsUserKeyDown(UserId, Keys.A)) {
                _mesh.Translate(-1 * (int) SpeedMultiplier, 0);
            }

            foreach (var pair in _keysMap)
            {
                if (input.IsUserKeyDown(UserId, pair.Key) && this.Inventory._items.ContainsKey(pair.Value))
                {
                    this.Inventory._items[pair.Value].Use(this);
                    this.Inventory._items.Remove(pair.Value);
                }
            }

            if (!commands.IsEmpty())
            {
                commands.Dequeue().Execute();
            }
        }

        public void Buff(IBuff buff)
        {
            if (_activeBuff != null)
            {
                _activeBuff.Undo(this);
            }

            _activeBuff = buff;
            _activeBuff.Apply(this);
        }

        public override bool IsDynamic()
        {
            return true;
        }
    }
}
