using System.Security.Cryptography.X509Certificates;
using WebServer.Server;
using WebServer.Server.HTTP_Request;
using WebServer.Server.Responses;
using WebServer.Server.View;

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
                x.MapGet("/html", (r) => new HtmlResponse("<h1 style=\"color:blue;\">Hello from my html response</h1>"));
                x.MapGet("/form", (r) => new HtmlResponse(Form.Html.Replace("{0}", "")));
                x.MapPost("/form", (r) =>
                {
                    var x = r.Body;
                    return new HtmlResponse(Form.Html.Replace("{0}", $"<h1>You have submitted: {x}</h1>"));
                });
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
