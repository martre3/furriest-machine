using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Maze.Game.Objects;
using Maze.Game.Objects.Map;

namespace Maze.Game
{
    public class TestSpeed
    {
        private Stopwatch watch = new Stopwatch();
        private GameState obj = new GameState();
        private double memory = 0.0;
        public void Test()
        {
            Process proc = Process.GetCurrentProcess();
            watch.Start();
            for (int i = 0; i < 500; i++)
            {
                obj.Register(new BrickWall());
            }
            watch.Stop();
            var elapsedTime = watch.ElapsedMilliseconds;
            watch.Reset();
            Console.WriteLine("Flyweight elapsed time: {0} ms", elapsedTime);
            Console.WriteLine("Flyweight used memory: {0} MB", Math.Round(proc.PrivateMemorySize64 / 1e+6, 2));
            proc.Dispose();
            watch.Start();
        }
    }
}
