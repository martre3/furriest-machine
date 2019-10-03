using System;
using System.Collections.Generic;
using Maze.Game.Objects;

namespace Maze.Game
{
    [Serializable]
    public class GameState
    {
        public int UserId { get; set; }
        public Dictionary<int, GameObject> GameObjects { get; private set; }

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
        
        private void RegisterOrUpdate(GameObject gameObject)
        {
            if (GameObjects.ContainsKey((int) gameObject.Id)) {
                GameObjects[(int) gameObject.Id].Sync(gameObject);

                return;
            }

            Register(gameObject);
        }
    }
}
