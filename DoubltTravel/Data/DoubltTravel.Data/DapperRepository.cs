namespace DoubltTravel.Data
{
    using System;
    using System.Data.SqlClient;

    public class DapperRepository
    {
        private string connectionString;

        public DapperRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected Func<SqlConnection> ConnectionFactory
        {
            get
            {
                return () => new SqlConnection(connectionString);
            }
        }
    }
}
