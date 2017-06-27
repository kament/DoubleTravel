namespace DoubltTravel.Data.AssistenceInfos
{
    using System.Data.SqlClient;
    using System.Linq;
    using Dapper;
    using Models;

    public class AssistenceInfoRepository : DapperRepository, IAssistenceInfoRepository
    {
        public AssistenceInfoRepository(string connectionString) 
            : base(connectionString)
        {
        }

        public AssistenceInfo GetById(int id)
        {
            AssistenceInfo assistence;

            using (SqlConnection connection = ConnectionFactory())
            {
                connection.Open();
                assistence = connection.Query<AssistenceInfo>("SELECT * FROM AssistenceInfo WHERE Id = @Id", new { Id = id}).First();
            }

            return assistence;
        }
    }
}
