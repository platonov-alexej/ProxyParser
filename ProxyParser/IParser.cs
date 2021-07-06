using ProxyParser.Models;
using System.Collections.Generic;

namespace ProxyParser
{
    interface IParser
    {
        List<ProxyInDb> Parse();
    }
}
