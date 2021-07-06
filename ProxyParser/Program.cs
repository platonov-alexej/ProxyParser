using ProxyParser.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyParser
{
    class Program
    {
        static void Main(string[] args)
        {
            ProxyFinder proxyFinder = new ProxyFinder();
            proxyFinder.Find();

            ProxyChecker proxyChecker = new ProxyChecker();
            proxyChecker.Verify();
        }
    }
}
