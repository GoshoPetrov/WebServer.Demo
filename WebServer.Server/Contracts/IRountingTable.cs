using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server.HTTP_Request;

namespace WebServer.Server.Contracts
{
    internal interface IRountingTable
    {
        IRountingTable Map(string url, Method method, Response response);
        IRountingTable MapGet(string url, Response response);
        IRountingTable MapPost(string url, Response response);
    }
}
