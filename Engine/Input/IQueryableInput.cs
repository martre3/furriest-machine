namespace Maze.Engine.Input
{
    public interface IQueryableInput<T>
    {
        bool IsKeyDown(T key);
    }
}
