using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cake.Alive
{
    internal class ExceptionHandler
    {
        public static void Handle(string url, Exception exception)
        {
            exception = exception.InnerException ?? throw new NotAliveException($"The request to {url} failed: {exception.Message}", exception);

            if (exception is HttpRequestException && exception.InnerException != null)
            {
                exception = exception.InnerException;
                throw new NotAliveException($"The request to {url} failed: {exception.Message}", exception);
            }
            if (exception is TaskCanceledException)
            {
                throw new NotAliveException($"The request to {url} timed out", exception);
            }
        }
    }
}