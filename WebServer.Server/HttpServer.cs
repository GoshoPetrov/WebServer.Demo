using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Net;
using System.Net.Sockets;
using System.Text;
using WebServer.Server.Contracts;
using WebServer.Server.HTTP;
using WebServer.Server.HTTP_Request;

namespace WebServer.Server
{
    public class HttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener serverListener;

        private readonly RoutingTable routes;

        public HttpServer(string ipAddress, int port, Action<IRountingTable> routingTableConfiguration)
        {

            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;
            this.serverListener = new TcpListener(this.ipAddress, port);

            routingTableConfiguration(routes = new RoutingTable());
        }

        public HttpServer(int port, Action<IRountingTable> config)
            :this("127.0.0.1", port, config)
        {
            
        }

        public HttpServer(Action<IRountingTable> config)
            :this(8080, config)
        {
            
        }
        public void Start()
        {
            this.serverListener.Start();

            Console.WriteLine($"Server started on port {port}");
            Console.WriteLine("Listening for requests ... ");
            while (true)
            {
                var connection = serverListener.AcceptTcpClient();
                var networkStream = connection.GetStream();
                var requestText = this.ReadRequest(networkStream);
                Console.WriteLine(requestText);

                var request = Request.Parse(requestText);
                var response = routes.MatchRequest(request);
                
            }
        }
        private void WriteResponse(NetworkStream networkStream, Response response)
        {

            var responseBytes = Encoding.UTF8.GetBytes(response.ToString());
            networkStream.Write(responseBytes);
        }


        private string ReadRequest(NetworkStream networkStream)
        {
            var bufferLingth = 1024;
            var buffer = new byte[bufferLingth];
            var requestBuilder = new StringBuilder();
            do
            {
                var bytesRead = networkStream.Read(buffer, 0, bufferLingth);
                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }
            while (networkStream.DataAvailable);

            return requestBuilder.ToString();
        }
    }
}
