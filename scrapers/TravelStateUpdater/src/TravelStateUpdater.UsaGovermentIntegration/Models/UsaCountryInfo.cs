using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelStateUpdater.UsaGovermentIntegration.Factories;

namespace TravelStateUpdater.UsaGovermentIntegration.Models
{
    public class UsaCountryInfo
    {
        public string Access { get; internal set; }

        public string GeneralInformation { get; internal set; }

        public string HagueAbductionConvention { get; internal set; }

        public string Return { get; internal set; }

        public IEnumerable<CountryRepresentativeDepartment> Representatives { get; internal set; }

        public string Attorney { get; internal set; }

        public string Mediaton { get; internal set; }
        public AssistenceInfo AssistanceInfo { get; internal set; }
    }
}
