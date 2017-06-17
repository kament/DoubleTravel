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
            string generalInformation = ParseGeneralInformation(ddElements);
            string hagueAbductionConvention = ParseHagueAbduction(ddElements);
            XElement access = ddElements[4];
            XElement retainingAndAttorney = ddElements[5];
            XElement mediaton = ddElements[6];

            XElement assistanceForUsaCitisens = xml.Elements()
                .FirstOrDefault(e => e.Attributes().Any(a => a.Name == "id" && a.Value == "componentbox_resourc"));
        }

        private string ParseHagueAbduction(List<XElement> ddElements)
        {
            XElement hagueAbductionConvention = ddElements[3];

            if(hagueAbductionConvention != null)
            {
                string text = hagueAbductionConvention.Value;

                return text;
            }
            else
            {
                logger.LogCritical("Cannot parse ParseHagueAbduction");

                return null;
            }
        }

        private string ParseGeneralInformation(List<XElement> ddElements)
        {
            XElement generalInformation = ddElements[1];

            string text = generalInformation.Value;

            return text;
        }

        private IEnumerable<CountryRepresentativeDepartment> ParseEmbassiesAndConsules(List<XElement> ddElements)
        {
            XElement embassiesAndConsules = ddElements[0];

            return embassiesAndConsules;
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
