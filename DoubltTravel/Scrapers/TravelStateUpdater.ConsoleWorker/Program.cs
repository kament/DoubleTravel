using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TravelStateUpdater.UsaGovermentIntegration;

namespace TravelStateUpdater.ConsoleWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            
            UsaGovermentApi api = new UsaGovermentApi(loggerFactory);
            //var country = api.CountriesList().First();

            api.CountryInfo("UZ").Wait();
        }
    }
}
