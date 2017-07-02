namespace DoubltTravel.Data.AssistenceInfos
{
    using System.Threading.Tasks;

    using Models;

    public class AssistenceInfoRepository : IAssistenceInfoRepository
    {
        private SqlConnectionWrapper connection;

        public AssistenceInfoRepository(IConnectionStringProvider provider)
        {
            connection = new SqlConnectionWrapper(provider.Value);
        }

        public Task<AssistenceInfo> GetByIdAsync(int id)
        {
            string getQuery = "SELECT * FROM AssistenceInfo WHERE Id = @Id";
            var parameters = new
            {
                Id = id
            };

            return connection.QuerySingleOrDefaultAsync<AssistenceInfo>(getQuery, parameters);
        }

        public async Task<int> InsertAsync(AssistenceInfo assistenceInfo)
        {
            string insertQuery = "INSERT INTO AssistenceInfo VALUES(@Email, @Fax, @Phone, @Globe, @Title)";
            return await connection.ExecuteAsync(insertQuery, assistenceInfo);
        }

        public Task<int> UpdateAsync(int id, AssistenceInfo assistenceInfo)
        {
            string updateQuery = @"UPDATE AssistenceInfo 
                                   SET Email = @Email,
                                       Fax = @Fax
                                       Phone = @Phone
                                       Title = @Title,
                                       Globe = @Globe
                                   WHERE Id = @Id";

            var parameters = new
            {
                Email = assistenceInfo.Email,
                Fax = assistenceInfo.Fax,
                Phone = assistenceInfo.Phone,
                Title = assistenceInfo.Title,
                Globe = assistenceInfo.Globe,
                Id = id
            };

            return connection.ExecuteAsync(updateQuery, parameters);
        }
    }
}
