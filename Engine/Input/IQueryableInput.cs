using System;

namespace Maze.Engine.Input
{
    public interface IQueryableInput<T>
    {
        bool IsKeyDown(T key);
        bool IsUserKeyDown(int userId, T key);
    }
}
