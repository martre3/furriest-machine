using System;

namespace Maze.Engine.Input
{
    public interface IEnumerableInput<T> : IInput<T> where T : Enum
    {
    }
}
