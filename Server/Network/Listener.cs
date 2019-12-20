using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Shared.communication;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Maze.Server.Events;
using Server.src.FileLogging;
using Maze.Engine.Input;
using Maze.Game;
using Server.src.singleton;

namespace Maze.Server.Network
{
    public class Listener
    {
        public static event EventHandler<RequestReceivedArguments> RequestReceived;
        CancellationTokenSource CTS = new CancellationTokenSource();
        BinaryFormatter Formatter = new BinaryFormatter();
        TcpListener server = null;

        public Listener(int port)
        {
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            server = new TcpListener(localAddr, port);
            server.Start();

            var instance = Logger.getInstance();
            instance.GetChain().PrintMessageToFile("Listenting...");
            var instance1 = Logger.getInstance();
            instance1.GetChain().PrintMessageToFile("haha exception");
            var instance2 = Logger.getInstance();
            instance2.GetChain().PrintMessageToFile("haha warning");
            TestSpeed test = new TestSpeed();
            test.Test();
            this.AcceptClientsAsync(server, this.CTS.Token);
        }

        async Task AcceptClientsAsync(TcpListener server, CancellationToken cancellationToken)
        {
            int connectionsCount = 0;

            while (!cancellationToken.IsCancellationRequested)
            {
                TcpClient client = await server.AcceptTcpClientAsync()
                                                    .ConfigureAwait(false);
        
                new Thread(() => HandleClient(client, connectionsCount++, cancellationToken)).Start();
            }
        }

        void HandleClient(TcpClient client, int clientIndex, CancellationToken ct)
        {

            var instance = Logger.getInstance();
            instance.GetChain().PrintMessageToFile(("New client ({0}) connected", clientIndex).ToString());

            try {
                using (client)
                {
                    Connection connection = new Connection(clientIndex, client.GetStream(), new BinaryFormatter());

                    while (!ct.IsCancellationRequested)
                    {
                        var data = (FormInput) connection.GetRequest();
                        Listener.RequestReceived(this, new RequestReceivedArguments(data, connection));
                    }
                }
            } catch (Exception e) {
                instance = Logger.getInstance();
                instance.GetChain().PrintMessageToFile(e.GetBaseException().ToString());
            }
            instance = Logger.getInstance();
            instance.GetChain().PrintMessageToFile(("Client ({0}) disconnected", clientIndex).ToString());
        }
    }
}
