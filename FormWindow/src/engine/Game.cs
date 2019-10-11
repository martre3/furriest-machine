using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.src.enums;
using System.Timers;
using System.Net.Sockets;
using System.Threading;
using Shared.communication;
using Shared.communication.ClientToServer;
using Shared.communication.enums;
using System.Runtime.Serialization.Formatters.Binary;
using FormWindow;
using System.Windows.Forms;

namespace Maze.src.engine
{
    class Game
    {
        private int FramesRendered = 0;
        private Renderer RenderEngine;
        private List<GameObject> Map = new List<GameObject>();
        private ConcurrentQueue<Request> RequestQueue = new ConcurrentQueue<Request>();

        public Game(GraphicsForm graphics)
        {
            GraphicsForm.Rendering += this.GameLoop;
            GraphicsForm.KeyPressed += this.
            this.RenderEngine = new Renderer(graphics);

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

                Request data = new Request() {
                    RequestType = Requests.Initial,
                };

                binaryFormatter.Serialize(stream, data);

                data = new SelectStyleRequest() {
                    RequestType = Requests.SelectStyle,
                    Style = Shared.Enums.MapStyle.Style2,
                };

                Request request;

                while ((request = (Request) binaryFormatter.Deserialize(stream)) != null)
                {
                    
                }
                binaryFormatter.Serialize(stream, data);

                data = new Request() {
                    RequestType = Requests.StartGame,
                };

                binaryFormatter.Serialize(stream, data);


                // while (true) 
                // {
                    
                // }

                var list = ((List<Shared.Engine.GameObject>) binaryFormatter.Deserialize(stream));

                System.Diagnostics.Debug.WriteLine(list.Count);

                list.ForEach(gameObject => factory.Create(gameObject).Insantiate());

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

        private void KeyPressed(object sender, KeyEventArgs arguments)
        {
            var request = this.GetRequest(arguments.KeyCode);

            if (request != null)
            {
                this.RequestQueue.Append(request);
            }
        } 

        private Request GetRequest(Keys keyCode)
        {
            switch (keyCode)
            {
                case Keys.Oem1:
                    return new SelectStyleRequest() {
                        RequestType = Requests.SelectStyle,
                        Style = Shared.Enums.MapStyle.Style1,
                    };
                case Keys.Oem2:
                    return new SelectStyleRequest() {
                        RequestType = Requests.SelectStyle,
                        Style = Shared.Enums.MapStyle.Style2,
                    };
                case Keys.Enter:
                    return new Request() {
                        RequestType = Requests.StartGame,
                    };
            }

            return null;
        }

        private void GameLoop(object sender, EventArgs e)
        {
            this.RenderEngine.Redraw();
        }
    }
}
