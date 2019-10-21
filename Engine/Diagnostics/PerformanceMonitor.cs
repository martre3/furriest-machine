using System;
using System.Diagnostics;
using Maze.Engine.Events;

namespace Maze.Engine.Diagnostics
{
    class PerformanceMonitor
    {
        /// <value>Number of (ms) it took to process the last frame.</value>
        public double LastFrameTime { get; private set; }

        /// <value>Numebr of (ms) into current frame.</value>
        public double CurrentFrameTime
        {
            get
            {
                return _stopwatch.Elapsed.TotalMilliseconds;
            }
        }

        /// <value>A derived frame rate based on how long it took to process last frame.</value>
        public double FrameRate {
            get
            {
                return LastFrameTime > 0 ? (1000 / LastFrameTime) : 0;
            }
        }

        private Stopwatch _stopwatch;

        public PerformanceMonitor()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Restart();
        }

        public void CaptureFrame(object sender, UpdateEventArgs e)
        {
            _stopwatch.Stop();
            LastFrameTime = _stopwatch.Elapsed.TotalMilliseconds;
            _stopwatch.Restart();
        }
    }
}
