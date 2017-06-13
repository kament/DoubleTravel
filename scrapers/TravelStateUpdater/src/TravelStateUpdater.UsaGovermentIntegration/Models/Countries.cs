using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelStateUpdater.UsaGovermentIntegration.Models
{
    public class CountriesList
    {
        public IEnumerable<UsaCountryModel> Countries { get; set; }
    }
}
