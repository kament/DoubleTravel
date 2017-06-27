namespace DoubltTravel.Data.Models
{
    using System.Collections.Generic;

    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int AssistenceIndoId { get; set; }

        public int CountryInfoId { get; set; }

        public AssistenceInfo AssistenceInfo { get; set; }

        public CountryInfo CountryInfo { get; set; }

        public IEnumerable<Representative> Representatives { get; set; }
    }
}
