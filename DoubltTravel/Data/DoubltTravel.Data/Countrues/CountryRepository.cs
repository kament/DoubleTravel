namespace DoubltTravel.Data.Countrues
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AssistenceInfos;
    using CountryInfo;
    using Models;

    public class CountryRepository : ICountryRepository
    {
        private SqlConnectionWrapper connection;
        private Lazy<IAssistenceInfoRepository> assistenceInfoRepository;
        private Lazy<ICountryInfoRepository> countryInfoRepository;

        public CountryRepository(IConnectionStringProvider provider, Lazy<IAssistenceInfoRepository> assistenceInfoRepository, Lazy<ICountryInfoRepository> countryInfoRepository)
        {
            connection = new SqlConnectionWrapper(provider.Value);
            this.assistenceInfoRepository = assistenceInfoRepository;
            this.countryInfoRepository = countryInfoRepository;
        }

        public async Task<IEnumerable<Country>> Countries()
        {
            return await connection.QueryAsync<Country>("SELECT * FROM Countrues");
        }

        public async Task<Country> CountryByIdAsync(int id)
        {
            var country = await connection.QuerySingleOrDefaultAsync<Country>("SELECT * FROM Countrues WHERE Id = @Id", new { Id = id });

            return country;
        }

        public async Task<int> InsertAsync(Country country)
        {
            int assistanceInfoId = await this.assistenceInfoRepository.Value.InsertAsync(country.AssistenceInfo);
            int countryInfoId = await this.countryInfoRepository.Value.InsertAsync(country.CountryInfo);

            string insertQuery = "INSERT INTO Countries VALUES(@Name, @Code, @AssistenceIndoId, @CountryInfoId) SELECT SCOPE_IDENTITY()";

            var parameters = new
            {
                Name = country.Name,
                Code = country.Code,
                AssistenceInfoId = assistanceInfoId,
                CountryInfoId = countryInfoId
            };

            return await connection.QuerySingleOrDefaultAsync<int>(insertQuery, parameters);
        }

        public async Task<Country> CountryByCodeAsync(string code)
        {
            string sql = @"SELECT * 
                           FROM Countrues c 
                           INNER JOIN CountryInfo cf
                                ON c.CountryInfoId = cf.Id
                           INNER JOIN AssistenceInfo af
                                ON c.AssistenceInfoId = af.Id
                           WHERE c.Code = @Code";

            var parameters = new
            {
                Code = code
            };

            Country result = await connection.QuerySingleOrDefaultAsync<Country, AssistenceInfo, CountryInfo, IEnumerable<Representative>, Country>
            (sql,
            (country, assistance, countryInfo, representatieves) =>
            {
                country.AssistenceInfo = assistance;
                country.CountryInfo = countryInfo;
                country.Representatives = representatieves;

                return country;
            }, 
            parameters);

            return result;
        }
    }
}
