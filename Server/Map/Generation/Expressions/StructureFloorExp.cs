using Maze.Server.Enums;

namespace Maze.Server.Map.Generation.Expressions
{
    public class StructureFloorExp : IExpression
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public StructureFloorExp(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Eval(MapContext context)
        {
            context.AddStructure(Structures.Floor, X, Y);
        }
    }
}
