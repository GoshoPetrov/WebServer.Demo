using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace WebServer.Server.HTTP_Request
{
    public class Response
    {
        public StatusCode StatusCode { get; init; }
        public HeaderCollection Headers { get;  } = new HeaderCollection();
        public string Body { get; set; }
        public Response(StatusCode statusCode)
        {
            this.StatusCode = statusCode;

            this.Headers.Add("Server", "My Web Server");
            this.Headers.Add("Data", $"{DateTime.UtcNow:r}");

            Headers.Add(Header.Server, "MyWebServer");
            Headers.Add(Header.Date, $"{DateTime.UtcNow : r}");
        }
    }
}
