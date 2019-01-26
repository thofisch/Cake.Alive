using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Alive.Tests.Builders
{
    internal class HttpContentBuilder
    {
        private static readonly Encoding DefaultEncoding = Encoding.UTF8;

        private readonly IDictionary<string, string> _headers = new Dictionary<string, string>();

        private byte[] _content = new byte[0];

        public HttpContentBuilder WithContent(string content, Encoding encoding = null)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            _content = (encoding ?? DefaultEncoding).GetBytes(content);
            return this;
        }

        public HttpContentBuilder WithContent(byte[] content)
        {
            _content = content ?? throw new ArgumentNullException(nameof(content));
            return this;
        }

        public HttpContentBuilder WithContentType(string value)
        {
            return AppendHeader("Content-Type", value);
        }

        public HttpContentBuilder AppendHeader(string name, string value)
        {
            _headers[name] = value;
            return this;
        }

        public HttpContent Build()
        {
            var httpContent = new FakeHttpContent(_content);

            foreach (var header in _headers)
            {
                httpContent.Headers.Add(header.Key, header.Value);
            }

            return httpContent;
        }

        public static implicit operator HttpContent(HttpContentBuilder builder)
        {
            return builder.Build();
        }

        #region FakeHttpContent class

        internal class FakeHttpContent : HttpContent
        {
            private readonly byte[] _content;

            public FakeHttpContent(byte[] content)
            {
                _content = content ?? throw new ArgumentNullException(nameof(content));
            }

            protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
            {
                return stream.WriteAsync(_content, 0, _content.Length);
            }

            protected override bool TryComputeLength(out long length)
            {
                length = _content.Length;
                return true;
            }
        }

        #endregion    }
    }
}