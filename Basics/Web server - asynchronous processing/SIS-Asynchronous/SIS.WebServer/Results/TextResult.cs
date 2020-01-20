namespace SIS.WebServer.Results
{
    using SIS.HTTP.Enums;
    using SIS.HTTP.Headers;
    using SIS.HTTP.Responses;
    using System.Text;

    public class TextResult : HttpResponse
    {
        private const string HeaderType = "Content-Type";
        private const string ContentType = "text/plain; charset=utf8";

        public TextResult(string content, HttpResponseStatusCode responseStatusCode,
            string contentType = ContentType)
            : base(responseStatusCode)
        {
            this.Headers.AddHeader(new HttpHeader(HeaderType, contentType));
            this.Content = Encoding.UTF8.GetBytes(content);
        }

        public TextResult(byte[] content, HttpResponseStatusCode responseStatusCode,
            string contentType = ContentType)
            : base(responseStatusCode)
        {
            this.Content = content;
            this.Headers.AddHeader(new HttpHeader(HeaderType, contentType));
        }
    }
}
