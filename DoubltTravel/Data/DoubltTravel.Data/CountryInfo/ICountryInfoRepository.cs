namespace DoubltTravel.Data.CountryInfo
{
    using System.Threading.Tasks;

    using Models;

    public interface ICountryInfoRepository
    {
        Task<CountryInfo> GetByIdAsync(int id);

        Task<int> InsertAsync(CountryInfo assistenceInfo);

        Task<int> UpdateAsync(int id, CountryInfo assistenceInfo);
    }
}
