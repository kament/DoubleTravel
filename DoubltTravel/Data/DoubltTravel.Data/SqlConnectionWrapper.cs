namespace DoubltTravel.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;

    public class SqlConnectionWrapper
    {
        private string connectionString;

        public SqlConnectionWrapper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<TResult>> QueryAsync<TResult>(string query)
        {
            IEnumerable<TResult> result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                result = await connection.QueryAsync<TResult>(query);

                connection.Close();
            }

            return result;
        }

        public async Task<IEnumerable<TResult>> QueryAsync<TResult>(string query, params object[] parameters)
        {
            IEnumerable<TResult> result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                result = await connection.QueryAsync<TResult>(query, parameters);

                connection.Close();
            }

            return result;
        }
        
        public async Task<TResult> QuerySingleOrDefaultAsync<TResult>(string query)
        {
            TResult result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                result = await connection.QuerySingleOrDefaultAsync<TResult>(query);

                connection.Close();
            }

            return result;
        }

        public async Task<TReturn> QuerySingleOrDefaultAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, string split, object param)
        {
            TReturn result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var results = await connection.QueryAsync(sql, map, param, splitOn: split);

                result = results.SingleOrDefault();

                connection.Close();
            }

            return result;
        }

        public async Task<TResult> QuerySingleOrDefaultAsync<TResult>(string query, params object[] parameters)
        {
            TResult result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                result = await connection.QuerySingleOrDefaultAsync<TResult>(query, parameters);

                connection.Close();
            }

            return result;
        }

        public async Task<int> ExecuteAsync(string query)
        {
            int result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                result = await connection.ExecuteAsync(query);

                connection.Close();
            }

            return result;
        }

        public async Task<int> ExecuteAsync(string query, params object[] parameters)
        {
            int result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(query, parameters);

                result = connection.Query<int>("SELECT @@IDENTITY").Single();
                connection.Close();
            }

            return result;
        }
    }
}
