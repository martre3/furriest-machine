﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Shared.communication;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Shared.communication.enums;
using MazeServer.src.server.Events;
using Shared.Engine;

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

            Console.WriteLine("Listenting...");
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
            Console.WriteLine("New client ({0}) connected", clientIndex);

            try {
                using (client)
                {
                    Connection connection = new Connection(client.GetStream());

                    while (!ct.IsCancellationRequested)
                    {
                        Request data = (Request) connection.GetRequest();
                        Server.RequestReceived(this, new RequestReceivedArguments(data, connection));
                    }
                }
            } catch (Exception e) {
                Console.WriteLine(e.GetBaseException());
            }
            Console.WriteLine("Client ({0}) disconnected", clientIndex);
        }
    }
}
