using System;

namespace Maze.Engine.Events
{
    public static class EventExtensions
    {
        public static void Raise<T>(this EventHandler<T> handler, object sender, T args)
        {
            if (handler != null)
            {
                handler(sender, args);
            }
        }
    }
}
