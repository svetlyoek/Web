namespace SIS.WebServer.Results
{
    using SIS.HTTP.Enums;
    using SIS.HTTP.Headers;
    using SIS.HTTP.Responses;

    public class RedirectResult : HttpResponse
    {
        private const string HeaderType = "Location";

        public RedirectResult(string location)
            : base(HttpResponseStatusCode.SeeOther)
        {
            this.Headers.AddHeader(new HttpHeader(HeaderType, location));
        }
    }
}
