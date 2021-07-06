using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyParser.Models
{
    public class ProxyInDb
    {
        public int id { get; set; }
        public string ProxyType { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public string Country { get; set; }
        public string Anonymity { get; set; }
    }
}
