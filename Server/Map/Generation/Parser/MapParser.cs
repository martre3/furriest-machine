using System;
using System.Collections.Generic;
using System.IO;
using Maze.Server.Map.Generation.Expressions;

namespace Maze.Server.Map.Generation.Parser
{
    public class MapParser
    {
        public List<IExpression> ParseMap(string path)
        {
            var expressions = new List<IExpression>();

            char c;
            int charCode;

            int lineCount = 0;
            int columnCount = 0;

            using (StreamReader file = new StreamReader(path)) {
                while (file.Peek() >= 0)
                {
                    charCode = file.Read();
                    c = (char) charCode;

                    // Line feed
                    if (charCode == 10)
                    {
                        lineCount++;
                        columnCount = 0;
                        continue;
                    }

                    if (
                        charCode == 9 // Tab
                        || charCode == 13 // Carriage return
                        || charCode == 32 // Space
                    )
                    {
                        continue;
                    }

                    expressions.Add(ParseChar(c, columnCount, lineCount));

                    columnCount++;
                }
            }

            return expressions;
        }

        private IExpression ParseChar(char c, int x, int y)
        {
            switch (c)
            {
                case '#':
                    return new StructureWallExp(x, y);
                case '=':
                    return new StructureFloorExp(x, y);
                default:
                    throw new NotImplementedException($"Not supported character '{(int) c}' found in the map, at ({x}, {y}).");
            }
        }
    }
}
