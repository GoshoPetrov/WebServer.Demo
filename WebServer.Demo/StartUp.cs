using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using WebServer.Server;
using WebServer.Server.HTTP;
using WebServer.Server.HTTP_Request;
using WebServer.Server.Responses;
using WebServer.Server.View;

namespace WebServer.demo
{
    internal class StartUp
    {
        static Dictionary<string, string> serverState = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            //var xx = new HtmlResponse("<html><h1 style=\"color:blue;\">Hello form my html response</h1></html>");
            //Console.WriteLine(xx.ToString());


            var server = new HttpServer(x =>
            {
                x.MapGet("/", (r) => new HtmlResponse("<h1 style=\"color:blue;\">Web server is running!</h1>"));
                x.MapGet("/html", (r) => new HtmlResponse("<h1 style=\"color:blue;\">Hello from my html response</h1>"));
                x.MapGet("/form", (r) => new HtmlResponse(Form.Html.Replace("{0}", "")));
                x.MapPost("/form", (r) =>
                {
                    var x = r.Body;
                    return new HtmlResponse(Form.Html.Replace("{0}", $"<h1>You have submitted: {x}</h1>"));
                });

                x.MapGet("/login", (r) => new DynamicResponse((response) =>
                {
                    response.Headers.Add("Set-Cookie", @$"session={Guid.NewGuid().ToString()}; Path=/; HttpOnly; Secure; SameSite=Lax");
                    
                    response.Body = @"<!DOCTYPE html>
<html>
<head>
  <title>Login</title>
</head>
<body>
  <form action=""/login"" method=""post"">
    <label>Username:</label>
    <input type=""text"" name=""username"" required>
    <br>
    <label>Password:</label>
    <input type=""password"" name=""password"" required>
    <br>
    <button type=""submit"">Submit</button>
  </form>
</body>
</html>";
                }));

                x.MapPost("/login", (request) => new DynamicResponse((response) =>
                {
                    var cookies = request.Cookies();

                    if (!cookies.ContainsKey("session"))
                    {
                        //TODO: or redirect to GET /login ?
                        response.Body = @"Error! No cookies. Go to <a href=""/login"">Login</a>!";
                        response.StatusCode = StatusCode.Unauthorized;
                        return;
                    }

                    var session = cookies["session"];

                    // remeber to add the session every time
                    response.Headers.Add("Set-Cookie", @$"session={session}; Path=/; HttpOnly; Secure; SameSite=Lax");

                    var form = request.Form();

                    if (!form.ContainsKey("username"))
                    {
                        response.Body = @"Error! username is missing. Go to <a href=""/login"">Login</a>!";
                        response.StatusCode = StatusCode.BadRequest;
                        return;
                    }


                    serverState[session] = form["username"];

                    response.Body = @$"<!DOCTYPE html>
<html>
<head>
  <title>Login</title>
</head>
<body>
    <h1>Wellcome, {form["username"]}!</h1>
    Session: {session}
</body>
</html>";
                }));

                x.MapGet("/logout", (request) => new DynamicResponse((response) =>
                {
                    var cookies = request.Cookies();

                    if (!cookies.ContainsKey("session"))
                    {
                        //TODO: or redirect to GET /login ?
                        response.Body = @"Error! No cookies. Go to <a href=""/login"">Login</a>!";
                        response.StatusCode = StatusCode.Unauthorized;
                        return;
                    }

                    var session = cookies["session"];

                    // remove the session cookie
                    response.Headers.Add("Set-Cookie", @$"session=; Path=/; HttpOnly; Secure; SameSite=Lax");

                    serverState.Remove(session);

                    response.Body = @"<!DOCTYPE html>
<html>
<head>
  <title>Logout</title>
</head>
<body>
    You are logged out!
</body>
</html>";
                }));
            });


            server.Start();


        }
    }
}
