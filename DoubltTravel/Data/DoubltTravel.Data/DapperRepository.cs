namespace DoubltTravel.Data
{
    using System;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public class DapperRepository
    {
        private string connectionString;

        public DapperRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<TResult> QueryAsync<TResult>(Func<SqlConnection, TResult> func)
        {
            TResult result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                result = func(connection);
            }

            return result;
        }
    }
}
