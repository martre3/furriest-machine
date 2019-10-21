using System;

namespace Maze.Engine.Events
{
    public class UpdateEventArgs : EventArgs
    {
        public double FrameRate { get; private set; }
        public double LastFrameTime { get; private set; }
        public float UpdateTimeStep { get; private set; }

        public UpdateEventArgs(
            double frameRate,
            double lastFrameTime,
            float updateTimeStep
        ) {
            FrameRate = frameRate;
            LastFrameTime = lastFrameTime;
            UpdateTimeStep = updateTimeStep;
        }
    }
}
