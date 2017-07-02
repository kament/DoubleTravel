using System;
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

            //ComputeAsync(container, api, countries);
            ComputeSync(container, api, countries).Wait();

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        private static void ComputeAsync(Container container, UsaGovermentApi api, IEnumerable<UsaCountryModel> countries)
        {
            var a = Parallel.ForEach(countries, async (country) =>
            {
                try
                {
                    UsaCountryInfo info = await api.CountryInfo(country.Code);
                    if (info != null)
                    {
                        CountryService service = container.GetInstance<CountryService>();

                        await service.AddOrUpdate(info, country);
                    }
                    else
                    {
                        Console.WriteLine($"No info for {country.Code}");
                    }
                }
                catch (System.Exception ex)
                {
                }
            });

            while (!a.IsCompleted)
            {
            }
        }

        private static async Task ComputeSync(Container container, UsaGovermentApi api, IEnumerable<UsaCountryModel> countries)
        {
            foreach(var country in countries)
            {
                try
                {
                    UsaCountryInfo info = await api.CountryInfo(country.Code);
                    if (info != null)
                    {
                        CountryService service = container.GetInstance<CountryService>();

                        await service.AddOrUpdate(info, country);
                    }
                    else
                    {
                        Console.WriteLine($"No info for {country.Code}");
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Failed for {country.Code} {ex.Message}");
                }
            }
        }
    }
}
