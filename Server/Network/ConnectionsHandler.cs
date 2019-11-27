using System;
using System.Net;
using System.Collections.Concurrent;

namespace Maze.Server.Network
{
    public class ConnectionsHandler
    {    
        public ConcurrentDictionary<int, IConnection> _connections = new ConcurrentDictionary<int, IConnection>();

        public bool IsInit(IConnection connection)
        {
            return !_connections.ContainsKey(connection.GetId());
        }

        public void Connect(IConnection connection)
        {
            if (!IsInit(connection)) {
                return;
            }

            _connections.TryAdd(connection.GetId(), connection);
        }

        public void Apply(Action<IConnection> action)
        {
            foreach (var pair in _connections)
            {
                action(pair.Value);
            }
        }
    }
}
