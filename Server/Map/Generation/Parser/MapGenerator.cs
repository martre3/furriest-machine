using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Server.Map.Generation.Parser
{
    public class MapGenerator
    {
        private List<string[,]> mapSplittedMatrixes;
        private readonly string ProjectDirectory = Environment.CurrentDirectory;

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

        public void printTofile(List<string[,]> matrix)
        {
            StreamWriter writer = File.AppendText($"{ProjectDirectory}/assets/maps/testMap123.txt");
            for (int i = 0; i < 4; i++)
            {
                int count = 0;
                for (int j = 0; j < 6; j++)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        writer.Write(matrix[(i * j) + count].ToString());
                    }
                }
                writer.WriteLine();
            }
        }

        public List<string[,]> getGeneratedMatrix()
        {
            return mapSplittedMatrixes;
        }
    }
}
