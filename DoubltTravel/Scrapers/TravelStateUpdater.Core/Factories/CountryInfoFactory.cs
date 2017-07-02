using DoubltTravel.Data.Models;
using TravelStateUpdater.UsaGovermentIntegration.Models;

namespace TravelStateUpdater.Core.Factories
{
    public static class CountryInfoFactory
    {
        public static CountryInfo Create(UsaCountryInfo sourceInfo)
        {
            var info = new CountryInfo
            {
                Access = sourceInfo.Access,
                Attorney = sourceInfo.Attorney,
                GeneralInformation = sourceInfo.GeneralInformation,
                HagueAbductionConvention = sourceInfo.HagueAbductionConvention,
                Mediaton = sourceInfo.Mediaton,
                Return = sourceInfo.Return
            };

            return info;
        }

        public static void Update(CountryInfo infoToUpdate, UsaCountryInfo sourceInfo)
        {
            infoToUpdate.Access = sourceInfo.Access;
            infoToUpdate.Attorney = sourceInfo.Attorney;
            infoToUpdate.GeneralInformation = sourceInfo.GeneralInformation;
            infoToUpdate.HagueAbductionConvention = sourceInfo.HagueAbductionConvention;
            infoToUpdate.Mediaton = sourceInfo.Mediaton;
            infoToUpdate.Return = sourceInfo.Return;
        }
    }
}
