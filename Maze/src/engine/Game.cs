using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;
using Maze.src.enums;
using System.Timers;
using System.Net.Sockets;
using System.Threading;
using Shared.communication;
using Shared.communication.enums;
using System.Runtime.Serialization.Formatters.Binary;

namespace Maze.src.engine
{
    class Game
    {
        private int FramesRendered = 0;
        private Renderer RenderEngine;
        private List<GameObject> Map = new List<GameObject>();

        public Game(Canvas canvas)
        {
            CompositionTarget.Rendering += this.GameLoop;
            this.RenderEngine = new Renderer(canvas);
            
            var fpsTimer = new System.Timers.Timer(1000) {
                AutoReset = true,
                Enabled = true,
            }; 
            fpsTimer.Elapsed += (object sender, ElapsedEventArgs e) => {
                Console.WriteLine($"{this.FramesRendered} fps");
                this.FramesRendered = 0;
            };

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Connect("127.0.0.1");
            }).Start();
        }

        private static void Connect(String server)
        {
            try
            {
                Int32 port = 1239;
                TcpClient client = new TcpClient(server, port);
                NetworkStream stream = client.GetStream();
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                GameObjectFactory factory = new GameObjectFactory();

                ClientToServer data = new ClientToServer() {
                    RequestType = Request.Initial,
                };

                binaryFormatter.Serialize(stream, data);

                ((List<Shared.Engine.GameObject>) binaryFormatter.Deserialize(stream)).ForEach(gameObject => factory.Create(gameObject).Insantiate());

                Console.WriteLine("Spawned");

                Thread.Sleep(7000);

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

        private void GameLoop(object sender, EventArgs e)
        {
            Console.WriteLine("NextLoop");
            this.RenderEngine.Redraw();

            this.FramesRendered++;
        }
    }
}
