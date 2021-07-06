using HtmlAgilityPack;
using ProxyParser.Models;
using ProxyParser.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProxyParser.Parser
{
    class HtmlWebRu : IParser
    {
        public void Parse()
        {
            WebClient myClient = new WebClient();
            List<ProxyInDb> proxiesToDb = new List<ProxyInDb>();

            for (int i = 1; i <= 30; i++)           // Я ограничился 30-ю страцами чтобы зря не дергать сайт если уж задание тестовое
            {
                Stream response = myClient.OpenRead($"https://htmlweb.ru/analiz/proxy_list.php?p={i}");
                StreamReader reader = new StreamReader(response);
                HtmlDocument doc = new HtmlDocument();
                doc.Load(reader);


                foreach (HtmlNode row in doc.DocumentNode.SelectNodes("//table").FirstOrDefault().SelectNodes("//tr"))
                {
                    if (row.HasClass("hand"))
                        continue;
                    var cells = row.SelectNodes("td");
                    if (cells.Count == 5)
                    {
                        ProxyInDb tmpProxy = new ProxyInDb();
                        string[] ipPortPair = cells[0].InnerText.Split(':');
                        tmpProxy.IP = ipPortPair[0];
                        tmpProxy.Port = Convert.ToInt32(ipPortPair[1]);

                        string port_anon = cells[4].InnerText;
                        if (port_anon.Contains("Анонимный"))
                        {
                            tmpProxy.Anonymity = "anonym";
                            port_anon = port_anon.Replace("Анонимный", "").Replace(",", "").Trim();
                        }
                        if (!string.IsNullOrWhiteSpace(port_anon))
                        {
                            tmpProxy.ProxyType = port_anon;
                        }

                        var imgTag = cells[1].SelectSingleNode("a").SelectSingleNode("img");
                        if (imgTag != null)
                        {
                            tmpProxy.Country = cells[1].SelectSingleNode("a").InnerText.Replace(imgTag.ToString(), "").Trim(new char[] { '"', ' ' });
                        }

                        proxiesToDb.Add(tmpProxy);
                    }
                }
            }

            DbWorker dbWorker = new DbWorker();
            Console.WriteLine($"{dbWorker.InsertProxyList(proxiesToDb).Result} was added to DB");
        }
    }
}
