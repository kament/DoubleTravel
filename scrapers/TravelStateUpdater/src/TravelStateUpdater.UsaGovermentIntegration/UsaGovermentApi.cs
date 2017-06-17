using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TravelStateUpdater.UsaGovermentIntegration.Models;

namespace TravelStateUpdater.UsaGovermentIntegration
{
    public class UsaGovermentApi
    {
        private string countryInfoUrl;
        private string filePath;

        public UsaGovermentApi()
        {
            filePath = @"C:\FmiProjects\WebSites\DoubltTravel\scrapers\TravelStateUpdater\src\TravelStateUpdater.UsaGovermentIntegration\countriesList.json";
            countryInfoUrl = @"https://travel.state.gov/content/travel/resources/database/database.getautoselectpage.html?cid={0}&aid=MainCSIs";
        }

        public IEnumerable<UsaCountryModel> CountriesList()
        {
            string countriesJson = File.ReadAllText(filePath);
            CountriesList countries = JsonConvert.DeserializeObject<CountriesList>(countriesJson);

            return countries.Countries;
        }

        public async Task<UsaCountryInfo> CountryInfo(string countryCode)
        {
            string url = string.Format(countryInfoUrl, countryCode);

            using (HttpClient client = new HttpClient())
            {
                Stream html = await client.GetStreamAsync(url);

                
            }
        }

        private static bool SuccessResponse(string json)
        {
            bool isSuccessMessage = !string.IsNullOrWhiteSpace(json) || json.ToLower() != "error";

            return isSuccessMessage;
        }
    }
}
