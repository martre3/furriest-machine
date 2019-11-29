using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Server.Game.Data;
using Maze.Server.Events;
using Maze.Engine;
using Maze.Server.Network;
using Maze.Server.Game.State;
using MazeServer.src;

using Maze.Engine.Input;
using Maze.Game;
using Maze.Server.Game;
using Maze.Engine.Physics;

namespace Maze.Server.Menu
{
    public class RoomsHandler
    {
        private Dictionary<int, GameStateContext> _rooms = new Dictionary<int, GameStateContext>();

        private Dictionary<int, int> _playerRoomMap = new Dictionary<int, int>();

        public RoomsHandler() 
        {
            Listener.RequestReceived += this.HandleRequest;
            // _rooms.Add(1, new GameStateContext(new RoomState(new GameData(game.GetGameState(), new InputHandler(input)))));
        }

        public void HandleRequest(object sender, RequestReceivedArguments args)
        {
            // if (_playerRoomMap.ContainsKey(args.Connection.GetId()))
            // {
            //     _rooms[_playerRoomMap[args.Connection.GetId()]].HandleRequest(args);
                
            //     return;
            // }


        }
    }
}
