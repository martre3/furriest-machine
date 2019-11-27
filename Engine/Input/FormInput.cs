using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Maze.Engine.Input
{
    [Serializable]
    public class FormInput : IQueryableFormInput
    {
        protected Dictionary<int, Dictionary<Keys, bool>> _inputKeys = new Dictionary<int, Dictionary<Keys, bool>>();

        public bool IsKeyDown(Keys key)
        {
            foreach (var id in _inputKeys.Keys)
            {
                if (IsUserKeyDown(id, key))
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsUserKeyDown(int userId, Keys key)
        {
            if (!_inputKeys.ContainsKey(userId)) {
                return false;
            }

            return _inputKeys[userId][key];
        }

        public void Merge(FormInput newInput)
        {
            foreach (var userInput in newInput._inputKeys)
            {
                _inputKeys[userInput.Key] = userInput.Value;
            }
        }

        public void Initialize(int userId)
        {
            _inputKeys[userId] = new Dictionary<Keys, bool>();

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                _inputKeys[userId][(Keys) key] = false;
            }
        }
    }
}
