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
        private List<GameObject> _temporaryList = new List<GameObject>();

        private bool _iterationInProgress = false;
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
                _iterationInProgress = true;

                foreach (var pair in GameObjects) {
                    callback(pair.Value);
                }

                _iterationInProgress = false;
                this.RegisterTemporary();
            }
        }

        public void Destroy(GameObject gameObject)
        {
            lock (_objectsLock) {
                GameObjects = new Dictionary<int, GameObject>();

                if (gameObject.IsDynamic())
                {
                    _dynamicGameObjects.Remove(gameObject);
                }

                GameObjects.Remove((int) gameObject.Id);
            }   
        }

        public virtual void Register(GameObject gameObject)
        {
            lock (_objectsLock) {
                if (_iterationInProgress)
                {
                    
                    _temporaryList.Add(gameObject);
                    
                    return;
                }

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
            if (GameObjects.ContainsKey(gameObject.Id.Value)) {
                GameObjects[gameObject.Id.Value].Sync(gameObject);

                return;
            }

            Register(gameObject);
        }

        private void RegisterTemporary()
        {
            lock (_objectsLock) {
                foreach (var gameObject in _temporaryList)
                {
                    this.Register(gameObject);
                }

                _temporaryList = new List<GameObject>();
            }
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
