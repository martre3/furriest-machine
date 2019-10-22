namespace Maze.Engine.Input
{
    public interface IInput<T>
    {
        void Initialize();
        void KeyDown(T key);
        void KeyUp(T key);
    }
}
