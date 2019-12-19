namespace Maze.Server.Map.Generation.Expressions
{
    public interface IExpression
    {
        int X { get; }
        int Y { get; }
        void Eval(MapContext context);
    }
}
