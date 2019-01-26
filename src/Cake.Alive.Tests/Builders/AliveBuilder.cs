using System.Net.Http;
using Cake.Core.Diagnostics;

namespace Cake.Alive.Tests.Builders
{
    internal class AliveBuilder
    {
        private readonly ICakeLog _log = new NullLog();

        private HttpClient _httpClient = new HttpClientBuilder();

        public AliveBuilder With(HttpClient httpClient)
        {
            _httpClient = httpClient;
            return this;
        }

        public Alive Build()
        {
            return new Alive(_log, _httpClient);
        }

        public static implicit operator Alive(AliveBuilder builder)
        {
            return builder.Build();
        }
    }
}