using System;
using System.Linq;
using System.Collections.Generic;
using Maze.Game.Objects;
using Maze.Engine.Physics;

namespace Maze.Game
{
    [Serializable]
    public class GameState: ICollidableContainer, ICloneable
    {
        public int UserId { get; set; }
        public Dictionary<int, GameObject> GameObjects { get; private set; }
        private List<ICollidable> _dynamicGameObjects = new List<ICollidable>();

        private int _currentId = 0;

        // [NonSerialized]
        private static object _objectsLock = new object();

        public GameState()
        {
            GameObjects = new Dictionary<int, GameObject>();
        }

        public void ApplyCallback(Action<GameObject> callback)
        {
            lock (_objectsLock) {
                foreach (var pair in GameObjects) {
                    callback(pair.Value);
                }
            }
        }

        public virtual void Register(GameObject gameObject)
        {
            lock (_objectsLock) {
                if (gameObject.Id == null) {
                    gameObject.Id = _currentId++;
                }

                if (gameObject.IsDynamic())
                {
                    _dynamicGameObjects.Add(gameObject);
                }

                GameObjects.Add((int) gameObject.Id, gameObject);
            }
        }

        public void SyncState(GameState newState)
        {
            UserId = newState.UserId;

            foreach (var o in newState.GameObjects.Values) {
                RegisterOrUpdate(o);
            }
        }

        public List<ICollidable> GetDynamicObjects()
        {
            return _dynamicGameObjects;
        }

        public List<ICollidable> GetAllColliders()
        {
            lock (_objectsLock) {
                return this.GameObjects.Values.Select(collider => (ICollidable) collider).ToList<ICollidable>();
            }
        }

        private void RegisterOrUpdate(GameObject gameObject)
        {
            if (GameObjects.ContainsKey((int) gameObject.Id)) {
                GameObjects[(int) gameObject.Id].Sync(gameObject);

                return;
            }

            Register(gameObject);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
