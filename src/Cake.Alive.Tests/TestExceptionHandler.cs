using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Cake.Alive.Tests
{
    public class TestExceptionHandler
    {
        [Fact]
        public void Can_handle_timeout()
        {
            var exception = Assert.Throws<NotAliveException>(() => ExceptionHandler.Handle("url", new AggregateException(new TaskCanceledException("cancelled"))));

            Assert.Equal("The request to url timed out", exception.Message);
        }

        [Fact]
        public void Can_handle_http_request_exceptions()
        {
            var exception = Assert.Throws<NotAliveException>(() => ExceptionHandler.Handle("url", new AggregateException(new HttpRequestException("hre", new WebException("webex")))));

            Assert.Equal("The request to url failed: webex", exception.Message);
        }

        [Fact]
        public void Can_handle_other_exceptions()
        {
            var exception = Assert.Throws<NotAliveException>(() => ExceptionHandler.Handle("url", new Exception("ex")));

            Assert.Equal("The request to url failed: ex", exception.Message);
        }
    }
}