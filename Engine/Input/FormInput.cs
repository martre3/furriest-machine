using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Maze.Engine.Input
{
    public class FormInput : IEnumerableInput<Keys>, IQueryableFormInput
    {
        private readonly Dictionary<Keys, bool> _inputKeys = new Dictionary<Keys, bool>();

        public void Initialize()
        {
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                _inputKeys[(Keys) key] = false;
            }
        }

        public bool IsKeyDown(Keys key)
        {
            return _inputKeys[key];
        }

        public void KeyDown(Keys key)
        {
            _inputKeys[key] = true;
        }

        public void KeyUp(Keys key)
        {
            _inputKeys[key] = false;
        }
    }
}
