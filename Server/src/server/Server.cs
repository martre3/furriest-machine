using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Shared.communication;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Shared.communication.enums;
using MazeServer.src.server.Events;
using Server.src.singleton;

namespace MazeServer.src.server
{
    public class Server
    {
        public static event EventHandler<RequestReceivedArguments> RequestReceived;

        CancellationTokenSource CTS = new CancellationTokenSource();
        BinaryFormatter Formatter = new BinaryFormatter();
        TcpListener server = null;

        public Server(int port)
        {
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            server = new TcpListener(localAddr, port);
            server.Start();

            var instance = Logger.getInstance();
            instance.printToFile("Listenting...");
            this.AcceptClientsAsync(server, this.CTS.Token);
        }

        async Task AcceptClientsAsync(TcpListener server, CancellationToken cancellationToken)
        {
            int connectionsCount = 0;

            while (!cancellationToken.IsCancellationRequested)
            {
                TcpClient client = await server.AcceptTcpClientAsync()
                                                    .ConfigureAwait(false);
        
                EchoAsync(client, connectionsCount++, cancellationToken);
            }
        }

        async Task EchoAsync(TcpClient client, int clientIndex, CancellationToken ct)
        {
            var instance = Logger.getInstance();
            instance.printToFile(("New client ({0}) connected", clientIndex).ToString());
            using (client)
            {
                Connection connection = new Connection(client.GetStream());

                while (!ct.IsCancellationRequested)
                {
                    ClientToServer data = (ClientToServer) connection.GetRequest();
                    Server.RequestReceived(this, new RequestReceivedArguments(data, connection));
                }
            }
            instance = Logger.getInstance();
            instance.printToFile(("Client ({0}) disconnected", clientIndex).ToString());
        }
    }
}
