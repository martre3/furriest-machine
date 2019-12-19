using System;
using System.Collections.Generic;
using Maze.Game.Objects.Map;
using Maze.Server.Map.Generation.Expressions;
using Maze.Server.Map.Generation.Parser;

namespace Maze.Server.Map.Generation
{
    public class MapGenerator: IMapGenerator
    {
        private readonly string _projectDirectory = Environment.CurrentDirectory;
        private MapParser _parser;

        public MapGenerator()
        {
            _parser = new MapParser();
        }

        public List<Structure> Generate(MapContext context)
        {
            List<IExpression> expressions = _parser.ParseMap($"{_projectDirectory}/assets/maps/testMap.txt");

            context.Reset();
            expressions.ForEach(exp => exp.Eval(context));

            return context.Structures;
        }
    }
}
