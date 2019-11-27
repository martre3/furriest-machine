using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Maze.Engine.Input
{
    [Serializable]
    public class ClientFormInput : FormInput
    {
        private int UserId = -1;

        public bool IsInitialized()
        {
            return UserId != -1;
        }

        public void KeyDown(Keys key)
        {
            if (IsInitialized())
            {
                _inputKeys[UserId][key] = true;
            }
        }

        public void KeyUp(Keys key)
        {
            if (IsInitialized())
            {
                _inputKeys[UserId][key] = false;
            }
        }

        public void SetUserId(int userId)
        {
            UserId = userId;
            this.Initialize(userId);
        }
    }
}
