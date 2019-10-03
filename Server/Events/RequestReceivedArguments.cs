using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.communication;
using Maze.Engine.Input;
using Maze.Server.Network;

namespace Maze.Server.Events
{
    public class RequestReceivedArguments: EventArgs
    {
        public FormInput Input { get; }
        public Connection Connection { get; }
        public RequestReceivedArguments(FormInput input, Connection connection)
        {
            Input = input;
            this.Connection = connection;
        }
    }
}
