using System;
using System.Collections;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Http;
using System.Collections.Specialized;

namespace PodeKestrel
{
    public class PodeRequest
    {
        public string HttpMethod { get; private set; }
        public string Host { get; private set; }
        public string ContentType { get; private set; }
        public long ContentLength { get; private set; }
        public string Protocol { get; private set; }
        public string ProtocolVersion { get; private set; }
        public Encoding ContentEncoding => System.Text.Encoding.UTF8;
        public string UserAgent { get; private set; }
        public string UrlReferrer { get; private set; }
        public string Body => ReadBody();
        public PodeForm Form => (new PodeForm(Request.Form));
        public Stream InputStream => Request.Body;
        public X509Certificate2 ClientCertificate => Request.HttpContext.Connection.ClientCertificate;

        public Uri Url { get; private set; }
        public EndPoint RemoteEndpoint { get; private set; }
        public IPEndPoint LocalEndpoint { get; private set; }
        public NameValueCollection QueryString { get; private set; }
        public Hashtable Headers { get; private set; }
        public HttpRequestException Error { get; set; }

        private HttpRequest Request;
        private PodeContext Context;


        public PodeRequest(HttpRequest request, PodeContext context)
        {
            Request = request;
            Context = context;

            // default values
            HttpMethod = Request.Method;
            Host = Request.Host.Value;
            ContentType = Request.ContentType;
            ContentLength = Request.ContentLength.HasValue ? Request.ContentLength.Value : 0;
            Protocol = Request.Scheme;
            ProtocolVersion = (Request.Protocol.Split('/')[1]);
            UserAgent = Request.Headers.ContainsKey("User-Agent") ? (string)Request.Headers["User-Agent"] : string.Empty;
            UrlReferrer = Request.Headers.ContainsKey("Referer") ? (string)Request.Headers["Referer"] : string.Empty;

            // protocol and url
            var _proto = (Request.IsHttps ? "https" : "http");
            Url = new Uri($"{_proto}://{Host}{Request.Path.Value}");

            RemoteEndpoint = new IPEndPoint(Request.HttpContext.Connection.RemoteIpAddress, Request.HttpContext.Connection.RemotePort);
            LocalEndpoint = new IPEndPoint(Request.HttpContext.Connection.LocalIpAddress, Request.HttpContext.Connection.LocalPort);

            // query string
            QueryString = new NameValueCollection();
            foreach (var key in Request.Query.Keys)
            {
                QueryString.Add(key, Request.Query[key]);
            }

            // headers
            Headers = new Hashtable();
            foreach (var key in Request.Headers.Keys)
            {
                Headers.Add(key, Request.Headers[key]);
            }

            // reset error
            Error = default(HttpRequestException);
        }

        public void ParseFormData() { }

        private string ReadBody()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                return reader.ReadToEnd();
            }
        }
    }
}