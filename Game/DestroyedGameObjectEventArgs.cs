using System.Threading.Tasks;
using Maze.Engine;
using Maze.Engine.Events;
using Maze.Game.Objects;
using System;

namespace Maze.Game
{
    public class DestroyedGameObjectEventArgs: EventArgs 
    {
        public GameObject GameObject { get; }

        public DestroyedGameObjectEventArgs(GameObject gameObject)
        {
            this.GameObject = gameObject;
        }
    }
}
