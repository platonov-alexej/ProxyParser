using ProxyParser.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                parser.Parse();
            }
        }
    }
}
