using System.Collections.Generic;
using DoubltTravel.Data.Models;

namespace DoubltTravel.Data.Countrues
{
    public interface ICountryRepository
    {
        void Insert(Country country);

        void Update(Country country);

        Country CountryById(int id);

        IEnumerable<Country> Countries();
    }
}