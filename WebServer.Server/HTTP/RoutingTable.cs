using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server.Contracts;
using WebServer.Server.HTTP_Request;

namespace WebServer.Server.HTTP
{
    internal class RoutingTable : IRountingTable
    {
        private readonly Dictionary<Method, Dictionary<string, Response>> routes;

        public RoutingTable()
        {
            routes = new Dictionary<Method, Dictionary<string, Response>>()
            {
                [Method.Get] = new Dictionary<string, Response>(),
                [Method.Post] = new Dictionary<string, Response>(),
                [Method.Delete] = new Dictionary<string, Response>(),
                [Method.Put] = new Dictionary<string, Response>()
            };
        }
        public IRountingTable Map(string url, Method method, Response response)
        {
            throw new NotImplementedException();
        }

        public IRountingTable MapGet(string url, Response response)
        {
            throw new NotImplementedException();
        }

        public IRountingTable MapPost(string url, Response response)
        {
            throw new NotImplementedException();
        }

        public Response MatchRequest(Request request)
        {
            var requestMethod = request.Method;
        }
    }
}
