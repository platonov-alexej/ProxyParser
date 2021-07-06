using ProxyParser.Parser;
using ProxyParser.Services;
using System;
using System.Collections.Generic;


namespace ProxyParser
{
    class ProxyFinder
    {
        List<IParser> parsers;

        public ProxyFinder()
        {
            parsers = new List<IParser>();
        }
        
        public void Find()
        {
            parsers.Add(new HtmlWebRu());

            foreach(IParser parser in parsers)
            {
                var proxies = parser.Parse();
                DbWorker dbWorker = new DbWorker();
                Console.WriteLine($"{dbWorker.InsertProxyList(proxies).Result} was added to DB");
            }
        }
    }
}
