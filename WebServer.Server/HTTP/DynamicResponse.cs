using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server.HTTP_Request;

namespace WebServer.Server.HTTP
{
    public class DynamicResponse: Response
    {
        private Action<DynamicResponse>? buildResponse;

        public DynamicResponse(Action<DynamicResponse> buildResponse)
            : base(StatusCode.OK)
        {
            this.buildResponse = buildResponse;
        }

        public override string ToString()
        {
            if (this.buildResponse != null)
            {
                // make sure we call the buildResponse only once
                this.buildResponse(this);
                this.buildResponse = null;
            }

            return base.ToString();
        }
    }
}
