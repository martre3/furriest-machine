using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using Maze.Engine;
using Maze.Engine.Input;
using Maze.Engine.Renderer;
using Maze.Game;
using System.Timers;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Maze.Client
{
    class StateSynchronizer
    {
        public StateSynchronizer(GameState state, ClientFormInput input)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Connect("127.0.0.1", input, state);
            }).Start();
        }

        private static void Connect(String server, ClientFormInput input, GameState state)
        {
            try
            {
                Int32 port = 1239;
                TcpClient client = new TcpClient(server, port);
                NetworkStream stream = client.GetStream();
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                while (true)
                {
                    binaryFormatter.Serialize(stream, input);

                    var newState = (GameState) binaryFormatter.Deserialize(stream);
                    
                    if (!input.IsInitialized()) {
                        input.SetUserId(newState.UserId);
                    }

                    state.SyncState(newState);
                }

            
               // stream.Close();
                // throw new Exception("sss");
                // client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
            Console.Read();
        }
    }
}
