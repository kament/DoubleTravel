using DoubltTravel.Data.Models;
using TravelStateUpdater.UsaGovermentIntegration.Models;

namespace TravelStateUpdater.Core.Factories
{
    public static class CountryFactory
    {
        public static Country Create(UsaCountryModel countryDto, AssistenceInfo assistanceInfo, CountryInfo countryInfo)
        {
            Country country = new Country
            {
                Name = countryDto.Value,
                Code = countryDto.Code,
                CountryInfo = countryInfo,
                AssistenceInfo = assistanceInfo
            };

            return country;
        }
    }
}
