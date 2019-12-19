using Maze.Server.Enums;

namespace Maze.Server.Map.Generation.Expressions
{
    public class StructureWallExp: IExpression
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public StructureWallExp(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Eval(MapContext context)
        {
            context.AddStructure(Structures.Wall, X, Y);
        }
    }
}
