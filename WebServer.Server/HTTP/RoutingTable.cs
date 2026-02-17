using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server.Common;
using WebServer.Server.Contracts;
using WebServer.Server.HTTP_Request;
using WebServer.Server.Responses;

namespace WebServer.Server.HTTP
{
    internal class RoutingTable : IRountingTable
    {
        private readonly Dictionary<Method, Dictionary<string, MyHandler>> routes;

        public RoutingTable()
        {
            routes = new Dictionary<Method, Dictionary<string, MyHandler>>()
            {
                [Method.Get] = new Dictionary<string, MyHandler>(),
                [Method.Post] = new Dictionary<string, MyHandler>(),
                [Method.Delete] = new Dictionary<string, MyHandler>(),
                [Method.Put] = new Dictionary<string, MyHandler>()
            };
        }
        public IRountingTable Map(string url, Method method, MyHandler response)
        {
            switch (method)
            {
                case Method.Get:
                    return MapGet(url, response);
                case Method.Post:
                    return MapPost(url, response);
                default:
                    throw new InvalidOperationException($"Method {method} is not suporrted");
            }
        }

        public IRountingTable MapGet(string url, MyHandler response)
        {
            Guard.AgainstNull(url, nameof(url));
            Guard.AgainstNull(response, nameof(response));

            this.routes[Method.Get][url] = response;

            return this;
        }

        public IRountingTable MapPost(string url, MyHandler response)
        {
            Guard.AgainstNull(url, nameof(url));
            Guard.AgainstNull(response, nameof(response));

            this.routes[Method.Post][url] = response;

            return this;
        }

        public MyHandler MatchRequest(Request request)
        {
            var requestMethod = request.Method;
            var requestUrl = request.Url;

            if(routes.ContainsKey(requestMethod) == false 
                || routes[requestMethod].ContainsKey(requestUrl) == false)
            {
                return (r) => new NotFoundResponse();
            }

            return routes[requestMethod][requestUrl];
        }
    }
}
