using System.IO;
using Microsoft.AspNetCore.Http;

namespace PodeKestrel
{
    public class PodeResponse
    {
        public int StatusCode
        {
            get => Response.StatusCode;
            set => Response.StatusCode = value;
        }

        public long ContentLength64
        {
            get => (Response.ContentLength.HasValue ? Response.ContentLength.Value : 0);
            set => Response.ContentLength = value;
        }

        public string ContentType
        {
            get => Response.ContentType;
            set => Response.ContentType = value;
        }

        public Stream OutputStream => Response.Body;
        public PodeResponseHeaders Headers { get; private set; }

        public string StatusDescription { get; set; }
        public bool SendChunked { get; set; }

        private HttpResponse Response;
        private PodeContext Context;


        public PodeResponse(HttpResponse response, PodeContext context)
        {
            Response = response;
            Context = context;
            Headers = new PodeResponseHeaders(response.Headers);
        }

        public void Close()
        {
            Response.Body.Dispose();
        }

    }
}