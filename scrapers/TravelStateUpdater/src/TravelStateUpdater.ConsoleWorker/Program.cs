using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelStateUpdater.UsaGovermentIntegration;

namespace TravelStateUpdater.ConsoleWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            UsaGovermentApi api = new UsaGovermentApi();
            var country = api.CountriesList().First();

            api.CountryInfo(country.Code).Wait();
        }
    }
}
