using System.IO;
using Microsoft.AspNetCore.Http;

namespace PodeKestrel
{
    public class PodeResponse
    {
        private int _statusCode = 200;
        public int StatusCode
        {
            get => _statusCode;
            set
            {
                if (!Sent)
                {
                    _statusCode = value;
                    Response.StatusCode = value;
                }
            }
        }

        private long _contentLength = 0;
        public long ContentLength64
        {
            get => _contentLength;
            set
            {
                if (!Sent)
                {
                    _contentLength = value;
                    Response.ContentLength = value;
                }
            }
        }

        private string _contentType = string.Empty;
        public string ContentType
        {
            get => _contentType;
            set
            {
                if (!Sent)
                {
                    _contentType = value;
                    Response.ContentType = value;
                }
            }
        }

        public bool Sent => Response.HasStarted;

        public Stream OutputStream { get; private set; }
        public PodeResponseHeaders Headers { get; private set; }

        public string StatusDescription { get; set; }
        public bool SendChunked { get; set; }

        private HttpResponse Response;
        private PodeContext Context;


        public PodeResponse(HttpResponse response, PodeContext context)
        {
            Response = response;
            Context = context;
            OutputStream = Response.Body;
            Headers = new PodeResponseHeaders(response.Headers);
        }

        public void Close()
        {
            Response.Body.Dispose();
            OutputStream = null;
        }

    }
}