using DoubltTravel.Data;
using DoubltTravel.Data.AssistenceInfos;
using DoubltTravel.Data.Countrues;
using DoubltTravel.Data.CountryInfo;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using TravelStateUpdater.Core;
using TravelStateUpdater.UsaGovermentIntegration;
using TravelStateUpdater.UsaGovermentIntegration.Factories;

namespace TravelStateUpdater.ConsoleWorker
{
    public class ContainerConfiguration
    {
        public static Container Bootstrap()
        {
            Container container = new Container();

            container.Register<IConnectionStringProvider, HardcodedConnectionStringProvider>();
            container.Register<ICountryInfoRepository, CountryInfoRepository>();
            container.Register<IAssistenceInfoRepository, AssistenceInfoRepository>();
            container.Register<ICountryRepository, CountryRepository>();
            container.Register<CountryService>();
            container.Register<CountryInfoHtmlFactory>();
            container.Register<UsaGovermentApi>();
            container.Register<ILoggerFactory, LoggerFactory>();

            container.Verify();

            return container;
        }
    }
}
