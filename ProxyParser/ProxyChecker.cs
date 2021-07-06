using ProxyParser.Models;
using ProxyParser.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProxyParser
{
    internal class ProxyChecker
    {
        public void Verify()
        {
            DbWorker dbWorker = new DbWorker();
            List<ProxyInDb> proxies = dbWorker.GetProxies();

            ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 10 };
            Parallel.ForEach(proxies, parallelOptions, VerifyServer);
        }

        private void VerifyServer(ProxyInDb proxy)
        {
            Pinger pinger = new Pinger();
            DbWorker localWorker = new DbWorker();
            try
            {
                if (!pinger.IsPing(proxy.IP))
                {                    
                    localWorker.DeleteById(proxy.id);
                    Console.WriteLine($"{proxy.IP} was deleted");
                }
                else
                {
                    Console.WriteLine($"{proxy.IP} confirm");
                }
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
                localWorker.DeleteById(proxy.id);
            }
        }
    }
}