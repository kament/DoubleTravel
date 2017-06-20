using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TravelStateUpdater.UsaGovermentIntegration.Factories;
using TravelStateUpdater.UsaGovermentIntegration.Models;

namespace TravelStateUpdater.UsaGovermentIntegration
{
    public class UsaGovermentApi
    {
        private string countryInfoUrl;
        private string filePath;
        private CountryInfoHtmlFactory factory;
        private string domain;

        public UsaGovermentApi(ILoggerFactory loggerFactory)
        {
            filePath = @"C:\FmiProjects\WebSites\DoubltTravel\scrapers\TravelStateUpdater\src\TravelStateUpdater.UsaGovermentIntegration\countriesList.json";
            domain = "https://travel.state.gov";
            countryInfoUrl = @"{0}/content/travel/resources/database/database.getautoselectpage.html?cid={1}&aid=MainCSIs";
            factory = new CountryInfoHtmlFactory(loggerFactory);
        }

        public IEnumerable<UsaCountryModel> CountriesList()
        {
            string countriesJson = File.ReadAllText(filePath);
            CountriesList countries = JsonConvert.DeserializeObject<CountriesList>(countriesJson);

            return countries.Countries;
        }

        public async Task<UsaCountryInfo> CountryInfo(string countryCode)
        {
            try
            {
                string url = string.Format(countryInfoUrl, domain, countryCode);

                using (HttpClient client = new HttpClient())
                {
                    string countryInfoPath = await client.GetStringAsync(url);
                    string html = await client.GetStringAsync(domain + countryInfoPath.Trim());

                    var doc = new HtmlDocument();
                    doc.LoadHtml(html);

                    UsaCountryInfo info = factory.Create(doc);

                    return info;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private static bool SuccessResponse(string json)
        {
            bool isSuccessMessage = !string.IsNullOrWhiteSpace(json) || json.ToLower() != "error";

            return isSuccessMessage;
        }
    }
}
