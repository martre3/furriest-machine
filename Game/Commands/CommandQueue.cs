using System;
using System.Collections.Generic;

namespace Maze.Game.Commands
{
    [Serializable]
    public class CommandQueue
    {
        private Queue<ICommand> _queue;

        public CommandQueue()
        {
            _queue = new Queue<ICommand>();
        }

        public void Enqueue(ICommand command)
        {
            _queue.Enqueue(command);
        }

        public ICommand Dequeue()
        {
            return _queue.Dequeue();
        }

        public bool IsEmpty()
        {
            return _queue.Count == 0;
        }
    }
}
