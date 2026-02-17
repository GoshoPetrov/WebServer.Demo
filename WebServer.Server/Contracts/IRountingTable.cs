using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server.HTTP_Request;

namespace WebServer.Server.Contracts
{
    public delegate Response MyHandler(Request request);


    public interface IRountingTable
    {
        IRountingTable Map(string url, Method method, MyHandler response);
        IRountingTable MapGet(string url, MyHandler response);
        IRountingTable MapPost(string url, MyHandler response);
    }
}
