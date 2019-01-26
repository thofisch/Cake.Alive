using System.Net;
using System.Net.Http;

namespace Cake.Alive.Tests.Builders
{
    internal class HttpResponseMessageBuilder
    {
        private readonly HttpResponseMessage _response = new HttpResponseMessage();

        public HttpResponseMessageBuilder WithStatus(HttpStatusCode status)
        {
            _response.StatusCode = status;
            return this;
        }

        public HttpResponseMessageBuilder WithContent(HttpContent content)
        {
            _response.Content = content;
            return this;
        }

        public HttpResponseMessageBuilder AppendHeader(string name, string value)
        {
            _response.Headers.Add(name, value);
            return this;
        }

        public HttpResponseMessage Build()
        {
            return _response;
        }

        public static implicit operator HttpResponseMessage(HttpResponseMessageBuilder builder)
        {
            return builder.Build();
        }
    }
}