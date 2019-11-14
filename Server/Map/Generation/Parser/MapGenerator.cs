using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Map.Generation.Parser
{
    class MapGenerator
    {
        private List<string[,]> mapSplittedMatrixes;

        public MapGenerator() 
        {
            mapSplittedMatrixes = new List<string[,]>();
        }

        public void generateMatrixList()
        {
            int iterationsCount = 24;
            Random rnd = new Random();
            for (int i = 0; i < iterationsCount; i++)
            {
                string[,] matrix = fillMatrixWithWalls();
                int row = rnd.Next(0, 4);
                int column = rnd.Next(0, 4);
                matrix[row, column] = "=";
                mapSplittedMatrixes.Add(matrix);
            }
        }

        public string[,] fillMatrixWithWalls()
        {
            int n = 5;
            string[,] matrix = new string[5,5];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = "#";
                }
            }
            return matrix;
        }

        public List<string[,]> getGeneratedMatrix()
        {
            return mapSplittedMatrixes;
        }
    }
}
