namespace SIS.WebServer
{
    using SIS.HTTP.Common;
    using SIS.HTTP.Cookies;
    using SIS.HTTP.Enums;
    using SIS.HTTP.Exceptions;
    using SIS.HTTP.Requests;
    using SIS.HTTP.Responses;
    using SIS.HTTP.Sessions;
    using SIS.WebServer.Results;
    using SIS.WebServer.Routing.Contracts;
    using System;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class ConnectionHandler
    {
        private readonly Socket client;
        private readonly IServerRoutingTable routingTable;
        private readonly HttpSessionStorage sessionStorage;

        public ConnectionHandler(Socket client, IServerRoutingTable routingTable)
        {
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(routingTable, nameof(routingTable));

            this.client = client;
            this.routingTable = routingTable;
        }
        public async Task ProcessRequestAsync()
        {
            try
            {
                var httpRequest = await this.ReadRequestAsync();

                if (httpRequest != null)
                {
                    Console.WriteLine($"Processing: {httpRequest.RequestMethod} {httpRequest.Path}...");
                    var sessionId = this.SetRequestSession(httpRequest);
                    var httpResponse = this.HandleRequest(httpRequest);
                    this.SetResponseSession(httpResponse, sessionId);
                    await this.PrepareResponse(httpResponse);
                }

            }
            catch (BadRequestException bre)
            {
                await this.PrepareResponse(new TextResult(bre.ToString(), HttpResponseStatusCode.BadRequest));

            }
            catch (Exception e)
            {
                await this.PrepareResponse(new TextResult(e.ToString(), HttpResponseStatusCode.InternalServerError));
            }

            this.client.Shutdown(SocketShutdown.Both);
        }
        private string SetRequestSession(IHttpRequest httpRequest)
        {
            string sessionId = null;

            if (httpRequest.Cookies.ContainsCookie(HttpSessionStorage.SessionCookieKey))
            {
                var cookie = httpRequest.Cookies.GetCookie(HttpSessionStorage.SessionCookieKey);
                sessionId = cookie.Value;
            }

            else
            {
                sessionId = Guid.NewGuid().ToString();
            }

            httpRequest.Session = HttpSessionStorage.GetSession(sessionId);
            return httpRequest.Session.Id;
        }

        private void SetResponseSession(IHttpResponse httpResponse, string sessionId)
        {
            if (sessionId != null)
            {
                httpResponse.AddCookie(new HttpCookie(HttpSessionStorage.SessionCookieKey, sessionId));
            }
        }
        private async Task<IHttpRequest> ReadRequestAsync()
        {
            var result = new StringBuilder();
            var data = new ArraySegment<byte>(new byte[1024]);

            while (true)
            {
                int bytesRead = await this.client.ReceiveAsync(data.Array, SocketFlags.None);

                if (bytesRead == 0)
                {
                    break;
                }

                var bytesAsString = Encoding.UTF8.GetString(data.Array, 0, bytesRead);
                result.Append(bytesAsString);

                if (bytesRead < 1023)
                {
                    break;
                }
            }

            if (result.Length == 0)
            {
                return null;
            }

            return new HttpRequest(result.ToString());
        }
        private IHttpResponse HandleRequest(IHttpRequest httpRequest)
        {
            if (!this.routingTable.Contains(httpRequest.RequestMethod, httpRequest.Path))
            {
                return new TextResult($"Route with method {httpRequest.RequestMethod} and path \"{httpRequest.Path}\" not found.", HttpResponseStatusCode.NotFound);
            }

            return this.routingTable.Get(httpRequest.RequestMethod, httpRequest.Path).Invoke(httpRequest);
        }
        private async Task PrepareResponse(IHttpResponse httpResponse)
        {
            byte[] byteSegments = httpResponse.GetBytes();
            this.client.Send(byteSegments, SocketFlags.None);
        }
    }
}
