﻿namespace SIS.WebServer
{
    using SIS.WebServer.Routing.Contracts;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;
    public class Server
    {
        public const string LocalhostIpAddress = "127.0.0.1";

        private readonly int port;
        private readonly TcpListener listener;
        private readonly IServerRoutingTable routingTable;
        private bool isRunning;

        public Server(int port, IServerRoutingTable routingTable)
        {
            this.port = port;
            this.routingTable = routingTable;

            this.listener = new TcpListener(IPAddress.Parse(LocalhostIpAddress), port);
        }

        public void Run()
        {
            this.listener.Start();
            this.isRunning = true;

            Console.WriteLine($"Server started at http//{LocalhostIpAddress}:{this.port}");

            while (this.isRunning)
            {
                Console.WriteLine("Waiting for client...");
                var client = this.listener.AcceptSocket();
                Task.Run(() => this.Listen(client));
            }
        }
        public async Task Listen(Socket client)
        {
            var connectionHandler = new ConnectionHandler(client, this.routingTable);
            await connectionHandler.ProcessRequestAsync();

        }
    }
}
