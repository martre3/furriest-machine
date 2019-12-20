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
using Maze.Game.Enums;
using Maze.Game.Mediators;

namespace Maze.Game.Objects
{
    [Serializable]
    public class Player: GameObject
    {
        [NonSerialized]
        public IBombMediator BombMediator;

        public int UserId { get; set; }
        public bool IsControlled { get { return this.UserId == this.state.UserId; }}

        // TODO: Should be float
        public float SpeedMultiplier { get; set; } = 1;
        protected override bool UsesCommands { get; } = true;
        public Inventory Inventory { get; }
        public List<IBuff> Buffs = new List<IBuff>();

        public float Health = 100;
        public PlayerRole Role;

        private Dictionary<Keys, int> _keysMap = new Dictionary<Keys, int>();
        private IBuff _activeBuff;

        [NonSerialized]
        private Brush _healthBarBackground;
        
        [NonSerialized]
        
        private Brush _healthBarBrush;

        private float _buffTimer = 4000;

        public Player(Point position, Inventory inventory): base()
        {
            this._mesh.SmoothSync = true;
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

            if (this.IsControlled)
            {
                if (_healthBarBackground == null)
                {
                    _healthBarBackground = new SolidBrush(Color.FromArgb(100, 255, 0, 0));
                    _healthBarBrush = new SolidBrush(Color.FromArgb(230, 255, 50, 50));
                }

                surface.FillRectangle(_healthBarBackground, 100 + this.Health * 2, 550, 200 - this.Health * 2, 15);
                surface.FillRectangle(_healthBarBrush, 100, 550, this.Health * 2, 15);
            }
        }

        public void OnBombExplosion(Bomb bomb)
        {
            if (bomb.GetDistanceTo(this) < 20)
            {
                bomb.ApplyEffect(this);
            }
        }

        public override void Update(IQueryableFormInput input, UpdateEventArgs e)
        {
            if (_activeBuff != null)
            {
                _buffTimer -= e.UpdateTimeStep;

                if (_buffTimer <= 0)
                {
                    _buffTimer = 4000;
                    _activeBuff.Undo(this);
                    _activeBuff = null;
                }
            }

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
                _buffTimer = 4000;
            }

            _activeBuff = buff;
            _activeBuff.Apply(this);
        }

        public override void Sync(GameObject gameObject)
        {
            base.Sync(gameObject);

            this.UserId = ((Player) gameObject).UserId;
            this.Health = ((Player) gameObject).Health;
        }

        public override bool IsDynamic()
        {
            return true;
        }

        public RegressMemento GetMemento()
        {
            return new RegressMemento(new Point(this.Position.X, this.Position.Y));
        }

        public void SetMemento(RegressMemento memento)
        {
            this._mesh.Position = new Point(memento.GetPoisition().X, memento.GetPoisition().Y);
            this._mesh.RealPosition = new Point(memento.GetPoisition().X, memento.GetPoisition().Y);
        }
    }
}
