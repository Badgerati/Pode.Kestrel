using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace PodeKestrel
{
    public class PodeContext : IDisposable
    {
        public string ID { get; private set; }
        public PodeRequest Request { get; private set; }
        public PodeResponse Response { get; private set; }
        public PodeListener Listener { get; private set; }
        public PodeSocket PodeSocket { get; private set;}
        public DateTime Timestamp { get; private set; }

        private HttpContext Context;
        private CancellationTokenSource ContextCancellationToken;
        private Task ContextTask;


        public PodeContext(HttpContext context, PodeListener listener)
        {
            ID = PodeHelpers.NewGuid();
            Context = context;
            Listener = listener;
            Timestamp = DateTime.UtcNow;

            Request = new PodeRequest(context.Request, this);
            Response = new PodeResponse(context.Response, this);

            PodeSocket = Listener.FindSocket(Request.LocalEndpoint);
            if (PodeSocket != default(PodeSocket) && !PodeSocket.CheckHostname(Request.Host))
            {
                Request.Error = new HttpRequestException($"Invalid request Host: {Request.Host}");
            }

            ContextCancellationToken = new CancellationTokenSource();
            ContextTask = new Task(() => {
                try
                {
                    var _task = Task.Delay(300000, ContextCancellationToken.Token);
                    _task.Wait();
                    Response.Close();
                    ContextCancellationToken.Cancel();
                }
                catch {}
            });
        }

        public Task Start()
        {
            ContextTask.Start();
            ContextTask.GetAwaiter();
            return ContextTask;
        }

        public void Dispose()
        {
            Response.Close();
            ContextCancellationToken.Cancel();
        }
    }
}