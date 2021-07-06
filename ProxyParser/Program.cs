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
