namespace SIS.HTTP.Requests
{
    using SIS.HTTP.Common;
    using SIS.HTTP.Enums;
    using SIS.HTTP.Exceptions;
    using SIS.HTTP.Headers;
    using SIS.HTTP.Headers.Contracts;
    using SIS.HTTP.Requests.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));

            this.FormData = new Dictionary<string, object>();
            this.QueryData = new Dictionary<string, object>();
            this.Headers = new HttpHeaderCollection();

            this.ParseRequest(requestString);

        }
        public string Path { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        private bool IsValidRequestLine(string[] requestLineParams)
        {
            if (requestLineParams.Length != 3 || requestLineParams[2] != GlobalConstants.HttpOneProtocolFragment)
            {
                return false;
            }

            return true;

        }
        private bool IsValidRequestQueryString(string queryString, string[] queryParameters)
        {
            CoreValidator.ThrowIfNullOrEmpty(queryString, nameof(queryString));

            if (queryParameters.Length < 1)
            {
                return false;
            }

            return true;

        }
        private void ParseRequestMethod(string[] requestLines)
        {
            bool isValidMethod = HttpRequestMethod.TryParse(requestLines[0], true, out HttpRequestMethod method);

            if (!isValidMethod)
            {
                throw new BadRequestException(string.Format(GlobalConstants.UnsupportedMethod, requestLines[0]));
            }
            this.RequestMethod = method;

        }
        private void ParseRequestUrl(string[] requestLines)
        {
            this.Url = requestLines[1];

        }
        private void ParseRequestPath()
        {
            this.Path = this.Url.Split('?')[0];
        }
        private void ParseRequestHeaders(string[] requestContent)
        {
            requestContent.Select(plainHeader => plainHeader.Split(new[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                .ToList()
                .ForEach(headerKVP => this.Headers.AddHeader(new HttpHeader(headerKVP[0], headerKVP[1])));
        }

        //TODO
        private void ParseCookies()
        {

        }
        private void ParseRequestQueryParameters()
        {
            if (this.HasQueryString())
            {
                this.Url.Split('?', '#')[1]
              .Split('&')
              .Select(queryParams => queryParams.Split('='))
              .ToList()
              .ForEach(queryKVP => this.QueryData.Add(queryKVP[0], queryKVP[1]));
            }
        }

        private bool HasQueryString()
        {
            return this.Url.Split('?').Length > 1;
        }

        //TODO Parse multiple parameters by name 
        private void ParseRequestFormDataParameters(string formData)
        {
            if (!string.IsNullOrEmpty(formData))
            {
                formData
               .Split('&')
               .Select(queryParams => queryParams.Split('='))
               .ToList()
               .ForEach(queryKVP => this.FormData.Add(queryKVP[0], queryKVP[1]));
            }
        }
        private IEnumerable<string> ParsePlainRequestHeaders(string[] requestLine)
        {
            for (int i = 1; i < requestLine.Length - 1; i++)
            {
                if (!string.IsNullOrEmpty(requestLine[i]))
                {
                    yield return requestLine[i];
                }
            }
        }
        private void ParseRequestParameters(string formData)
        {
            this.ParseRequestQueryParameters();
            this.ParseRequestFormDataParameters(formData);

        }
        private void ParseRequest(string requestString)
        {
            string[] splitRequestContent = requestString
                .Split(new[] { GlobalConstants.HttpNewLine }, StringSplitOptions.None);

            string[] requestLineParams = splitRequestContent[0]
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!IsValidRequestLine(requestLineParams))
            {
                throw new BadRequestException();
            }

            this.ParseRequestMethod(requestLineParams);
            this.ParseRequestUrl(requestLineParams);
            this.ParseRequestPath();

            this.ParseRequestHeaders(this.ParsePlainRequestHeaders(splitRequestContent).ToArray());
            this.ParseCookies();

            this.ParseRequestParameters(splitRequestContent[splitRequestContent.Length - 1]);

        }

    }
}
