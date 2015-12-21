using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Chess
{
    public static class Connection
    {
        public const int PORT = 123; // May be changed

        public static List<IPAddress> GetLocalIps()
        {
            List<IPAddress> ips = new List<IPAddress>();
            foreach ( IPAddress ip in Dns.GetHostAddresses( Dns.GetHostName() ) )
            {
                if ( ip.AddressFamily == AddressFamily.InterNetwork )
                    ips.Add( ip );
            }
            return ips;
        }

        public static Socket CreateSocket()
        {
            return new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
        }

    }
}
