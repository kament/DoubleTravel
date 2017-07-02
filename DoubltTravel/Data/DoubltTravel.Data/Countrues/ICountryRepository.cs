using System.Collections.Generic;
using System.Threading.Tasks;
using DoubltTravel.Data.Models;

namespace DoubltTravel.Data.Countrues
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> CountriesAsync();

        Task<Country> CountryByIdAsync(int id);

        Task<Country> CountryByCodeAsync(string code);
                
        Task<int> InsertAsync(Country country);
    }
}