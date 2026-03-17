using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace WebServer.Server.HTTP_Request
{
    public class Request
    {
        public Method Method { get; private set; }
        public string Url { get; private set; }
        public HeaderCollection Headers { get; private set; }
        public string Body { get; private set; }
        public static Request Parse(string request)
        {
            var lines = request.Split("\r\n");
            var startLine = lines.First().Split(" ");

            var method = ParseMethod(startLine[0]);

            var url = startLine[1];

            var headers = ParseHeaders(lines.Skip(1));

            var bodyLines = lines.Skip(headers.Count + 2).ToArray();
            var body = string.Join("\r\n", bodyLines);

            return new Request
            {
                Method = method,
                Url = url,
                Headers = headers,
                Body = body
            };
        }
        private static Method ParseMethod(string method)
        {
            try
            {
                return (Method)Enum.Parse(typeof(Method), method, true);
            }
            catch (Exception)
            {
                throw new InvalidOperationException($"Method '{method}' is not supportrd");
            }
        }
        private static HeaderCollection ParseHeaders(IEnumerable<string> headerLines)
        {
            var headers = new HeaderCollection();
            foreach (var headerLine in headerLines)
            {
                if(headerLine == string.Empty)
                {
                    break;
                }
                var headerParts = headerLine.Split(":", 2);
                if(headerParts.Length != 2)
                {
                    throw new InvalidOperationException("Request is not valid.");

                }

                var headerName = headerParts[0];
                var headerValue = headerParts[1].Trim();

                headers.Add(headerName, headerValue);
            }
            return headers;
        }

        public Dictionary<string, string> Cookies()
        {
            var cookieHeader = this.Headers
                .FirstOrDefault(h => h.Name.Equals("Cookie", StringComparison.OrdinalIgnoreCase));

            if (cookieHeader == null)
            {
                return new Dictionary<string, string>() ;
            }

            var cookies = cookieHeader.Value.Split(';');

            var result = new Dictionary<string, string>();

            foreach (var cookie in cookies)
            {
                var parts = cookie.Split('=', 2);

                if (parts.Length == 2)
                {
                    var name = parts[0].Trim();
                    var value = parts[1].Trim();

                    if (result.ContainsKey(name))
                    {
                        //TODO: array values...
                        result[name] = value;
                    } 
                    else
                    {
                        result.Add(name, value);
                    }
                        

                }
            }

            return result;
        }

        public Dictionary<string, string> Form()
        {
            var result = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(this.Body))
            {
                return result;
            }

            var pairs = this.Body.Split('&', StringSplitOptions.RemoveEmptyEntries);

            foreach (var pair in pairs)
            {
                var parts = pair.Split('=', 2);

                if (parts.Length != 2)
                {
                    continue; // or throw if you want strict parsing
                }

                var key = WebUtility.UrlDecode(parts[0]);
                var value = WebUtility.UrlDecode(parts[1]);

                result[key] = value;
            }

            return result;
        }
    }
}
