using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using TravelStateUpdater.UsaGovermentIntegration.Models;

namespace TravelStateUpdater.UsaGovermentIntegration.Factories
{
    public class CountryInfoHtmlFactory
    {
        private ILogger logger;

        public CountryInfoHtmlFactory(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger("CountryInfoHtmlFactory");
        }

        public UsaCountryInfo Create(HtmlDocument xml)
        {
            HtmlNode generalInfoContainer = FindGeneralInfoContainer(xml);

            if (generalInfoContainer != null)
            {
                List<HtmlNode> ddElements = GetDdElements(generalInfoContainer);

                UsaCountryInfo info = PopulateCountryInfoModel(xml, ddElements);

                return info;
            }
            else
            {
                logger.LogCritical("Couldn't parse the xml because \"expandos\" container doesn't exists");

                return null;
            }
        }

        private UsaCountryInfo PopulateCountryInfoModel(HtmlDocument xml, List<HtmlNode> ddElements)
        {
            IEnumerable<CountryRepresentativeDepartment> representatives = ParseEmbassiesAndConsules(ddElements);
            string generalInformation = GetString(ddElements[1], "ParseGeneralInformation");
            string hagueAbductionConvention = GetString(ddElements[2], "ParseHagueAbduction");
            string @return = GetString(ddElements[3], "Return");
            string access = GetString(ddElements[4], "Visitation/Access");
            string attorney = GetString(ddElements[5], "Retaining an Attorney");
            string mediaton = GetString(ddElements[6], "Mediation");

            AssistenceInfo assistanceInfo = GenerateAssistenceInfo(xml);

            return new UsaCountryInfo()
            {
                Representatives = representatives,
                GeneralInformation = generalInformation,
                HagueAbductionConvention = hagueAbductionConvention,
                Return = @return,
                Access = access,
                Attorney = attorney,
                Mediaton = mediaton,
                AssistanceInfo = assistanceInfo
            };
        }

        private AssistenceInfo GenerateAssistenceInfo(HtmlDocument xml)
        {
            IEnumerable<HtmlNode> assistanceForUsaCitisens = xml.DocumentNode.Descendants()
                 .FirstOrDefault(e => e.Attributes["id"]?.Value == "componentbox_resourc").Descendants();

            if (assistanceForUsaCitisens != null)
            {
                string name = assistanceForUsaCitisens.First(x => x.Name == "p" && x.Attributes["class"]?.Value == "emphasize")?.InnerText?.Trim();
                string phone = assistanceForUsaCitisens.First(x => x.Name == "li" && x.Attributes["class"]?.Value == "icon_phone")?.InnerText?.Trim();
                string fax = assistanceForUsaCitisens.First(x => x.Name == "li" && x.Attributes["class"]?.Value == "icon_fax")?.InnerText?.Trim();
                string email = assistanceForUsaCitisens.First(x => x.Name == "li" && x.Attributes["class"]?.Value == "icon_email")?.InnerText?.Trim();
                string globe = assistanceForUsaCitisens.First(x => x.Name == "li" && x.Attributes["class"]?.Value == "icon_globe")?.InnerText?.Trim();

                return new AssistenceInfo(name, phone, fax, email, globe);
            }
            else
            {
                logger.LogCritical("Cannot parse GenerateAssistenceInfo");

                throw new Exception("Parse GenerateAssistenceInfo fail, parsing cannot continue!");
            }
        }

        private string GetString(HtmlNode element, string valueName)
        {
            if (element != null)
            {
                string text = element.InnerText?.Trim();

                return text;
            }
            else
            {
                logger.LogCritical("Cannot parse {0}", valueName);

                return null;
            }
        }

        private IEnumerable<CountryRepresentativeDepartment> ParseEmbassiesAndConsules(List<HtmlNode> ddElements)
        {
            IEnumerable<HtmlNode> embassiesAndConsules = ddElements[0]?.Descendants().Where(e => e.Name == "div")
                .Where(e => e.Attributes["class"]?.Value == "simple_richtextarea section");

            foreach (var element in embassiesAndConsules)
            {
                string address = element.Descendants().FirstOrDefault(e => e.Attributes["class"]?.Value == "address")?.InnerText?.Trim();
                string phone = element.Descendants().FirstOrDefault(e => e.Attributes["class"]?.Value == "phone")?.InnerText?.Trim();
                string fax = element.Descendants().FirstOrDefault(e => e.Attributes["class"]?.Value == "fax")?.InnerText?.Trim();
                string email = element.Descendants().FirstOrDefault(e => e.Attributes["class"]?.Value == "email")?.InnerText?.Trim();

                yield return new CountryRepresentativeDepartment
                {
                    Address = address,
                    Phone = phone,
                    Fax = fax,
                    Email = email
                };
            }
        }

        private List<HtmlNode> GetDdElements(HtmlNode generalInfoContainer)
        {
            return generalInfoContainer.Descendants()
                .Where(e => e.Name == "dd")
                .ToList();
        }

        private HtmlNode FindGeneralInfoContainer(HtmlDocument xml)
        {
            return xml.DocumentNode.Descendants()
                .FirstOrDefault(e =>
                    e.Attributes
                        .Any(a => a.Name == "class" && a.Value == "expandos"));
        }
    }
}
