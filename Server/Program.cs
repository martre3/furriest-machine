using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using MazeServer.src.engine;
using MazeServer.src.server;
using MazeServer.src;

namespace MazeServer
{
    class Program
    {
        static void Main(string[] args)
        {
            new Services();
            new Game();
        }
    }
}
