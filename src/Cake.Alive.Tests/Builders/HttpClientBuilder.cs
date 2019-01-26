using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Cake.Alive.Tests.Builders
{
    internal class HttpClientBuilder
    {
        private HttpResponseMessage _response = new HttpResponseMessage();

        public HttpClientBuilder With(HttpResponseMessage response)
        {
            _response = response;
            return this;
        }

        public HttpClient Build()
        {
            return new HttpClient(new FakeHttpMessageHandler(_response));
        }

        public static implicit operator HttpClient(HttpClientBuilder builder)
        {
            return builder.Build();
        }

        #region FakeHttpMessageHandler class

        private class FakeHttpMessageHandler : HttpMessageHandler
        {
            private readonly HttpResponseMessage _response;

            public FakeHttpMessageHandler(HttpResponseMessage response)
            {
                _response = response;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var responseTask = new TaskCompletionSource<HttpResponseMessage>();
                responseTask.SetResult(_response);

                return responseTask.Task;
            }
        }

        #endregion
    }
}