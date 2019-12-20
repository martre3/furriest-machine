using System;
using Maze.Game.Objects;

namespace Maze.Game.Mediators
{
    public interface IBombMediator
    {   
        void Register(GameObject o);
        void Explode(Bomb bomb);
    }
}
