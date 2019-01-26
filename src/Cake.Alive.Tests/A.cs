using Cake.Alive.Tests.Builders;

namespace Cake.Alive.Tests
{
    internal static class A
    {
        public static AliveBuilder Alive => new AliveBuilder();
        public static HttpClientBuilder HttpClient => new HttpClientBuilder();
        public static HttpContentBuilder HttpContent => new HttpContentBuilder();
        public static HttpResponseMessageBuilder HttpResponseMessage => new HttpResponseMessageBuilder();
    }
}