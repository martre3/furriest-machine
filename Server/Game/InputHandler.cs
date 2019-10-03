using System;
using System.Net;
using Maze.Engine.Input;

namespace Maze.Server.Game
{
    public class InputHandler
    {    
        private FormInput _input;

        public InputHandler(FormInput input)
        {
            _input = input;
        }
        
        public void Merge(FormInput input)
        {
            _input.Merge(input);
        }
    }
}
