namespace DoubltTravel.Data.Countrues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AssistenceInfos;
    using CountryInfo;
    using Models;

    public class CountryRepository : ICountryRepository
    {
        private SqlConnectionWrapper connection;
        private IAssistenceInfoRepository assistenceInfoRepository;
        private ICountryInfoRepository countryInfoRepository;
        
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
            string sql = @"SELECT c.Id, c.Name, c.Code, af.Email as Email, af.Fax, af.Phone, af.Title, af.Globe, cf.Access as Access, cf.GeneralInformation, cf.HagueAbductionConvention, cf.[Return], cf.Attorney, cf.Mediaton
                           FROM Countries c 
                           INNER JOIN CountryInfo cf
                                ON c.CountryInfoId = cf.Id
                           INNER JOIN AssistenceInfo af
                                ON c.AssistenceInfoId = af.Id
                           WHERE c.Id = @Id";

            var parameters = new
            {
                Id = id
            };

            Country result = await connection.QuerySingleOrDefaultAsync<Country, AssistenceInfo, CountryInfo, Country>
            (sql,
            (country, assistance, countryInfo) =>
            {
                country.AssistenceInfo = assistance;
                country.CountryInfo = countryInfo;

                return country;
            },
            "Email, Access",
            parameters);

            return result;
        }

        public async Task<int> InsertAsync(Country country)
        {
            int assistanceInfoId = await this.assistenceInfoRepository.InsertAsync(country.AssistenceInfo);
            int countryInfoId = await this.countryInfoRepository.InsertAsync(country.CountryInfo);
            
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
            string sql = @"SELECT c.Id, c.Name, c.Code, af.Email as Email, af.Fax, af.Phone, af.Title, af.Globe, cf.Access as Access, cf.GeneralInformation, cf.HagueAbductionConvention, cf.[Return], cf.Attorney, cf.Mediaton
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
            "Email, Access",
            parameters);

            return result;
        }
    }
}
