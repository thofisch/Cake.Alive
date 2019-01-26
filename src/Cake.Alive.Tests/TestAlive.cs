using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cake.Alive.Tests
{
    public class TestAlive
    {
        [Fact]
        public void Can_handle_responses_with_an_successful_status_code()
        {
            Alive sut = A.Alive;

            sut.HttpPing("http://url", new AliveSettings());
        }

        [Fact]
        public void Can_handle_responses_with_an_unsuccessful_status_code()
        {
            Alive sut = A.Alive
                .With(A.HttpClient
                    .With(A.HttpResponseMessage
                        .WithStatus(HttpStatusCode.BadRequest)
                    )
                );

            Assert.Throws<NotAliveException>(() => sut.HttpPing("http://url", new AliveSettings()));
        }

        [Fact]
        public void Can_handle_exceptions()
        {
            Alive sut = A.Alive.With(new FakeHttpClient(new Exception()));

            Assert.Throws<NotAliveException>(() => sut.HttpPing("http://url", new AliveSettings()));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Url_must_not_be_null_or_empty(string url)
        {
            Alive sut = A.Alive;

            Assert.Throws<ArgumentException>(() => sut.HttpPing(url, new AliveSettings()));
        }

        [Fact]
        public void Settings_must_not_be_null()
        {
            Alive sut = A.Alive;

            Assert.Throws<ArgumentNullException>(() => sut.HttpPing("url", null));
        }

        [Fact]
        public void Can_set_timeout_from_settings()
        {
            HttpClient httpClient = A.HttpClient;
            Alive sut = A.Alive.With(httpClient);

            sut.HttpPing("http://url", new AliveSettings { Timeout = 1 });

            Assert.Equal(httpClient.Timeout, TimeSpan.FromMilliseconds(1));
        }

        private class FakeHttpClient : HttpClient
        {
            private readonly Exception _exception;

            public FakeHttpClient(Exception exception)
            {
                _exception = exception;
            }

            public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                throw _exception;
            }
        }
    }
}