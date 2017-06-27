using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DoubltTravel.Data.Models;

namespace DoubltTravel.Data.Countrues
{
    public class CountryRepository : DapperRepository, ICountryRepository
    {
        public CountryRepository(string connectionString) 
            : base(connectionString)
        {
        }

        public async Task<IEnumerable<Country>> Countries()
        {
            return await await QueryAsync(async (connection) =>
             {
                 IEnumerable<Country> countries = await connection.QueryAsync<Country>("SELECT * FROM Countrues");
                 return countries;
             });
        }

        public Country CountryById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Country country)
        {
            throw new NotImplementedException();
        }

        public void Update(Country country)
        {
            throw new NotImplementedException();
        }
    }
}
