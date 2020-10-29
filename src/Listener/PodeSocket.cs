using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace PodeKestrel
{
    public class PodeSocket
    {
        public IPAddress IPAddress { get; private set; }
        public List<string> Hostnames { get; private set; }
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

        public bool HasHostnames => Hostnames.Any();

        public PodeSocket(IPAddress ipAddress, int port, SslProtocols protocols, X509Certificate certificate = null, bool allowClientCertificate = false)
        {
            IPAddress = ipAddress;
            Port = port;
            Certificate = certificate;
            AllowClientCertificate = allowClientCertificate;
            Protocols = protocols;
            Hostnames = new List<string>();
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

        public bool CheckHostname(string hostname)
        {
            if (!HasHostnames)
            {
                return true;
            }

            var _name = hostname.Split(':')[0];
            return Hostnames.Any(x => x.Equals(_name, StringComparison.InvariantCultureIgnoreCase));
        }

        public new bool Equals(object obj)
        {
            var _socket = (PodeSocket)obj;
            return (IPAddress.ToString() == _socket.IPAddress.ToString() && Port == _socket.Port);
        }

        public bool Equals(IPEndPoint ipEndpoint)
        {
            return (IPAddress.ToString() == ipEndpoint.Address.ToString() && Port == ipEndpoint.Port);
        }
    }
}