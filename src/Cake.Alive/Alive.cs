using System;
using System.Diagnostics;
using System.Net.Http;
using Cake.Core.Diagnostics;

namespace Cake.Alive
{
    internal class Alive : IDisposable
    {
        private readonly ICakeLog _log;
        private readonly HttpClient _client;

        public Alive(ICakeLog log) : this(log, new HttpClient())
        {
        }

        internal Alive(ICakeLog log, HttpClient client)
        {
            _log = log ?? throw new ArgumentNullException(nameof(client));
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public void HttpPing(string url, AliveSettings settings)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(url));
            }
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            try
            {
                if (settings.Timeout > 0)
                {
                    _client.Timeout = TimeSpan.FromMilliseconds(settings.Timeout);
                }

                var sw = Stopwatch.StartNew();
                var response = _client.GetAsync(url).Result;
                sw.Stop();

                _log.Debug(response);

                if (response.IsSuccessStatusCode)
                {
                    _log.Information($"{url} responded with {response.StatusCode:D} ({response.ReasonPhrase}) after {sw.ElapsedMilliseconds} ms");

                    return;
                }

                throw new NotAliveException($"The request to {url} failed with response status code {response.StatusCode:D} ({response.ReasonPhrase})");
            }
            catch (AggregateException aggregateException)
            {
                ExceptionHandler.Handle(url, aggregateException);
            }
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}