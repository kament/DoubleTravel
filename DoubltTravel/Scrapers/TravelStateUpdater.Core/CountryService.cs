using System;
using System.Threading.Tasks;
using DoubltTravel.Data.AssistenceInfos;
using DoubltTravel.Data.Countrues;
using DoubltTravel.Data.CountryInfo;
using DoubltTravel.Data.Models;
using TravelStateUpdater.Core.Factories;
using TravelStateUpdater.UsaGovermentIntegration.Models;

namespace TravelStateUpdater.Core
{
    public class CountryService
    {
        private IAssistenceInfoRepository assistanceRepository;
        private ICountryInfoRepository countryInfoRepository;
        private ICountryRepository countryRepository;

        public CountryService(IAssistenceInfoRepository assistanceRepository, 
            ICountryRepository countryRepository,
            ICountryInfoRepository countryInfoRepository)
        {
            this.assistanceRepository = assistanceRepository;
            this.countryRepository = countryRepository;
            this.countryInfoRepository = countryInfoRepository;
        }

        public async Task AddOrUpdate(UsaCountryInfo countryInfo, UsaCountryModel country)
        {
            Country dbCountry = await countryRepository.CountryByCodeAsync(country.Code);
            if(dbCountry == null)
            {
                await Add(countryInfo, country);
            }
            else
            {
                await Update(dbCountry, countryInfo);
            }
        }

        private async Task Update(Country dbCountry, UsaCountryInfo countryInfo)
        {
            AssistanceInfoFactory.Update(dbCountry.AssistenceInfo, countryInfo.AssistanceInfo);
            CountryInfoFactory.Update(dbCountry.CountryInfo, countryInfo);

            await assistanceRepository.UpdateAsync(dbCountry.Id, dbCountry.AssistenceInfo);
            await countryInfoRepository.UpdateAsync(dbCountry.Id, dbCountry.CountryInfo);

            //TODO: Update Representatieves too
        }

        private async Task Add(UsaCountryInfo countryInfo, UsaCountryModel country)
        {
            Country countryToInsert = CountryFactory.Create(country, AssistanceInfoFactory.Create(countryInfo.AssistanceInfo), CountryInfoFactory.Create(countryInfo));
            await countryRepository.InsertAsync(countryToInsert);
        }
    }
}
