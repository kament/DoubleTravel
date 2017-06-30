namespace DoubltTravel.Data.CountryInfo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;

    public class CountryInfoRepository : ICountryInfoRepository
    {
        public Task<CountryInfo> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(CountryInfo assistenceInfo)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(int id, CountryInfo assistenceInfo)
        {
            throw new NotImplementedException();
        }
    }
}
