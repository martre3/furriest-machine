using System.Collections.Generic;
using Maze.Game.Objects;

namespace Maze.Game
{
    public class GameState
    {
        public List<GameObject> GameObjects { get; private set; }

        public GameState()
        {
            GameObjects = new List<GameObject>();
        }

        public void Register(GameObject gameObject)
        {
            GameObjects.Add(gameObject);
        }
    }
}
