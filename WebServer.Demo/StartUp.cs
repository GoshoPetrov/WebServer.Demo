using System.Security.Cryptography.X509Certificates;
using WebServer.Server;
using WebServer.Server.Responses;

namespace WebServer.demo
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            //var xx = new HtmlResponse("<html><h1 style=\"color:blue;\">Hello form my html response</h1></html>");
            //Console.WriteLine(xx.ToString());


            var server = new HttpServer(x =>
            {
                x.MapGet("/html", new HtmlResponse("<h1 style=\"color:blue;\">Hello form my html response</h1>"));
            });
            server.Start();



            //Action<string> print = (string input) => 
            //{
            //    Console.WriteLine(input);
            //};

            //print("Hello");


        }
    }
}
