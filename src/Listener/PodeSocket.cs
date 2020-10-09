using System.Net;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace PodeKestrel
{
    public class PodeSocket
    {
        public IPAddress IPAddress { get; private set; }
        public string Hostname { get; set; }
        public int Port { get; private set; }
        public X509Certificate Certificate { get; private set; }
        public bool AllowClientCertificate { get; private set; }
        public SslProtocols Protocols { get; private set; }
        public Socket Socket { get; private set; }
        public int ReceiveTimeout { get; set; }

        private PodeListener Listener;

        public bool IsSsl
        {
            get => (Certificate != default(X509Certificate));
        }

        public PodeSocket(IPAddress ipAddress, int port, SslProtocols protocols, X509Certificate certificate = null, bool allowClientCertificate = false)
        {
            IPAddress = ipAddress;
            Port = port;
            Certificate = certificate;
            AllowClientCertificate = allowClientCertificate;
            Protocols = protocols;
        }

        public void BindListener(PodeListener listener)
        {
            Listener = listener;
        }

        public void Listen(KestrelServerOptions options)
        {
            options.Listen(IPAddress, Port, (listenOpts) => {
                if (IsSsl)
                {
                    listenOpts.UseHttps((X509Certificate2)Certificate);
                }
            });
        }
    }
}