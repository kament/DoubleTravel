using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleInjector;
using TravelStateUpdater.Core;
using TravelStateUpdater.UsaGovermentIntegration;
using TravelStateUpdater.UsaGovermentIntegration.Models;

namespace TravelStateUpdater.ConsoleWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Container container = ContainerConfiguration.Bootstrap();
            UsaGovermentApi api = container.GetInstance<UsaGovermentApi>();

            IEnumerable<UsaCountryModel> countries = api.CountriesList();

            Parallel.ForEach(countries, async (country) =>
            {
                try
                {
                    UsaCountryInfo info = await api.CountryInfo(country.Code);
                    CountryService service = container.GetInstance<CountryService>();

                    await service.AddOrUpdate(info, country);
                }
                catch (System.Exception ex)
                {
                }
            });
        }
    }
}
