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
        private IAssistenceInfoRepository assistenceInfoRepository;
        private ICountryInfoRepository countryInfoRepository;

        private static int assistanceInfoId = 0;
        private static int countryInfoId = 0;

        public CountryRepository(IConnectionStringProvider provider, IAssistenceInfoRepository assistenceInfoRepository, ICountryInfoRepository countryInfoRepository)
        {
            connection = new SqlConnectionWrapper(provider.Value);
            this.assistenceInfoRepository = assistenceInfoRepository;
            this.countryInfoRepository = countryInfoRepository;
        }

        public async Task<IEnumerable<Country>> CountriesAsync()
        {
            return await connection.QueryAsync<Country>("SELECT * FROM Countries");
        }

        public async Task<Country> CountryByIdAsync(int id)
        {
            var country = await connection.QuerySingleOrDefaultAsync<Country>("SELECT * FROM Countries WHERE Id = @Id", new { Id = id });

            return country;
        }

        public async Task<int> InsertAsync(Country country)
        {
            await this.assistenceInfoRepository.InsertAsync(country.AssistenceInfo);
            await this.countryInfoRepository.InsertAsync(country.CountryInfo);

            assistanceInfoId += 1;
            countryInfoId += 1;
            string insertQuery = "INSERT INTO Countries VALUES(@Name, @Code, @AssistenceInfoId, @CountryInfoId)";

            var parameters = new
            {
                Name = country.Name,
                Code = country.Code,
                AssistenceInfoId = assistanceInfoId,
                CountryInfoId = countryInfoId
            };

            return await connection.ExecuteAsync(insertQuery, parameters);
        }

        public async Task<Country> CountryByCodeAsync(string code)
        {
            string sql = @"SELECT * 
                           FROM Countries c 
                           INNER JOIN CountryInfo cf
                                ON c.CountryInfoId = cf.Id
                           INNER JOIN AssistenceInfo af
                                ON c.AssistenceInfoId = af.Id
                           WHERE c.Code = @Code";

            var parameters = new
            {
                Code = code
            };

            Country result = await connection.QuerySingleOrDefaultAsync<Country, AssistenceInfo, CountryInfo, Country>
            (sql,
            (country, assistance, countryInfo) =>
            {
                country.AssistenceInfo = assistance;
                country.CountryInfo = countryInfo;

                return country;
            }, 
            parameters);

            return result;
        }
    }
}
