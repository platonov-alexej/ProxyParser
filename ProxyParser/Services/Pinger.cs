using System.Net.NetworkInformation;
using System.Net;

namespace ProxyParser.Services
{
    class Pinger
    {
        Ping ping;
        PingReply pingReply;

        public Pinger()
        {
            ping = new Ping();
            pingReply = null;
        }

        public bool IsPing(string serverAddress)
        {
            IPAddress ipAddress = IPAddress.Parse(serverAddress);
            pingReply = ping.Send(ipAddress);
            if (pingReply.Status == IPStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
