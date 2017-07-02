namespace DoubltTravel.Data.CountryInfo
{
    using System.Threading.Tasks;
    using Models;

    public class CountryInfoRepository : ICountryInfoRepository
    {
        private SqlConnectionWrapper connection;

        public CountryInfoRepository(string connectionString)
        {
            connection = new SqlConnectionWrapper(connectionString);
        }

        public async Task<CountryInfo> GetByIdAsync(int id)
        {
            var countryInfo = await connection.QuerySingleOrDefaultAsync<CountryInfo>("SELECT * FROM CountryInfo WHERE Id = @Id", new { Id = id });

            return countryInfo;
        }

        public async Task<int> InsertAsync(CountryInfo assistenceInfo)
        {
            string insertQuery = "INSERT INTO CountryInfo VALUES(@Access, @GeneralInformation, @HagueAbductionConvention, @Return, @Attorney, @Mediaton) SELECT SCOPE_IDENTITY()";

            int id = await connection.QuerySingleOrDefaultAsync<int>(insertQuery, assistenceInfo);

            return id;
        }

        public async Task<int> UpdateAsync(int id, CountryInfo assistenceInfo)
        {
            string updateQuery = @"UPDATE CountryInfo
                                   SET Access = @Access
                                       GeneralInformation = @GeneralInformation
                                       HagueAbductionConvention = @HagueAbductionConvention
                                       Return = @Return
                                       Attorney = @Attorney
                                       Mediaton = @Mediaton
                                   WHERE Id = @id";

            var parameters = new
            {
                Access = assistenceInfo.Access,
                GeneralInformation = assistenceInfo.GeneralInformation,
                HagueAbductionConvention = assistenceInfo.HagueAbductionConvention,
                Return = assistenceInfo.Return,
                Attorney = assistenceInfo.Attorney,
                Mediaton = assistenceInfo.Mediaton,
                Id = id
            };

            return await connection.ExecuteAsync(updateQuery, assistenceInfo);
        }
    }
}
