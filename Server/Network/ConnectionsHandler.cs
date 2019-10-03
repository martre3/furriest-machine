using System;
using System.Net;
using System.Collections.Concurrent;

namespace Maze.Server.Network
{
    public class ConnectionsHandler
    {    
        private ConcurrentDictionary<int, Connection> _connections = new ConcurrentDictionary<int, Connection>();

        public bool IsInit(Connection connection)
        {
            return !_connections.ContainsKey(connection.Id);
        }

        public void Connect(Connection connection)
        {
            if (!IsInit(connection)) {
                return;
            }

            _connections.TryAdd(connection.Id, connection);
        }

        public void Apply(Action<Connection> action)
        {
            foreach (var pair in _connections)
            {
                action(pair.Value);
            }
        }
    }
}
