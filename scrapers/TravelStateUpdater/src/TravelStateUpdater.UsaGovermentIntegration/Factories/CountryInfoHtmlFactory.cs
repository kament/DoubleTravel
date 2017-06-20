using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        public UsaCountryInfo Create(XElement xml)
        {
            XElement generalInfoContainer = FindGeneralInfoContainer(xml);

            if (generalInfoContainer != null)
            {
                List<XElement> ddElements = GetDdElements(generalInfoContainer);

                UsaCountryInfo info = PopulateCountryInfoModel(xml, ddElements);

                return info;
            }
            else
            {
                logger.LogCritical("Couldn't parse the xml because \"expandos\" container doesn't exists");

                return null;
            }
        }

        private UsaCountryInfo PopulateCountryInfoModel(XElement xml, List<XElement> ddElements)
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
                Mediaton = mediaton
            };
        }

        private AssistenceInfo GenerateAssistenceInfo(XElement xml)
        {
            IEnumerable<XElement> assistanceForUsaCitisens = xml.Elements()
                 .FirstOrDefault(e => e.Attributes().Any(a => a.Name == "id" && a.Value == "componentbox_resourc")).Elements();

            if (assistanceForUsaCitisens != null)
            {
                string name = assistanceForUsaCitisens.First(x => x.Name == "p" && x.Attribute("class")?.Value == "emphasize")?.Value;
                string phone = assistanceForUsaCitisens.First(x => x.Name == "li" && x.Attribute("class")?.Value == "icon_phone")?.Value;
                string fax = assistanceForUsaCitisens.First(x => x.Name == "li" && x.Attribute("class")?.Value == "icon_fax")?.Value;
                string email = assistanceForUsaCitisens.First(x => x.Name == "li" && x.Attribute("class")?.Value == "icon_email")?.Value;
                string globe = assistanceForUsaCitisens.First(x => x.Name == "li" && x.Attribute("class")?.Value == "icon_globe")?.Value;

                return new AssistenceInfo(name, phone, fax, email, globe);
            }
            else
            {
                logger.LogCritical("Cannot parse GenerateAssistenceInfo");

                throw new Exception("Parse GenerateAssistenceInfo fail, parsing cannot continue!");
            }
        }

        private string GetString(XElement element, string valueName)
        {
            if (element != null)
            {
                string text = element.Value;

                return text;
            }
            else
            {
                logger.LogCritical("Cannot parse {0}", valueName);

                return null;
            }
        }

        private IEnumerable<CountryRepresentativeDepartment> ParseEmbassiesAndConsules(List<XElement> ddElements)
        {
            IEnumerable<XElement> embassiesAndConsules = ddElements[0]?.Elements().Where(e => e.Name == "div")
                .Where(e => e.Attribute("class")?.Value == "simple_richtextarea section");

            foreach (var element in embassiesAndConsules)
            {
                string address = element.Elements().FirstOrDefault(e => e.Attribute("class")?.Value == "address")?.Value;
                string phone = element.Elements().FirstOrDefault(e => e.Attribute("class")?.Value == "phone")?.Value;
                string fax = element.Elements().FirstOrDefault(e => e.Attribute("class")?.Value == "fax")?.Value;
                string email = element.Elements().FirstOrDefault(e => e.Attribute("class")?.Value == "email")?.Value;

                yield return new CountryRepresentativeDepartment
                {
                    Address = address,
                    Phone = phone,
                    Fax = fax,
                    Email = email
                };
            }
        }

        private List<XElement> GetDdElements(XElement generalInfoContainer)
        {
            return generalInfoContainer.Elements()
                .Where(e => e.Name == "dd")
                .ToList();
        }

        private XElement FindGeneralInfoContainer(XElement xml)
        {
            return xml.Elements()
                .FirstOrDefault(e =>
                    e.Attributes()
                        .Any(a => a.Name == "class" && a.Value == "expandos"));
        }
    }
}
