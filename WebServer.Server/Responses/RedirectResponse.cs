using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server.HTTP_Request;

namespace WebServer.Server.Responses
{
    internal class RedirectResponse : Response
    {
        public RedirectResponse(string location) : base(StatusCode.Found)
        {
            Headers.Add(Header.Location, location);
        }
    }
}
