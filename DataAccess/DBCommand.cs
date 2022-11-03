using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using IDbCommand = Interface.IDbCommand;

namespace DataAccess
{
    public class DBCommand : IDbCommand
    {
        private readonly IDBConnection _dbConnection;

        public IDbConnection Connection => _dbConnection.Connection;

        public DBCommand()
        {
            _dbConnection = new DBConnection();
        }

        public async Task<DataTable> GetData(string query)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand(query, _dbConnection.Connection))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
            }

            return dt;
        }

        public async Task<int> UpdateAndInsertData(string query, List<SqlParameter> parameters, IDbTransaction transaction = null)
        {
            int numberOfRowsAffected = 0;

            using (SqlCommand cmd = new SqlCommand(query, _dbConnection.Connection))
            {
                if (_dbConnection.Connection.State == ConnectionState.Closed)
                {
                    await _dbConnection.OpenConnection();
                }
                    

                cmd.CommandType = CommandType.Text;
                cmd.Transaction = (SqlTransaction)transaction;
                if (parameters != null)
                {
                    parameters.ForEach(parameter =>
                    {
                        cmd.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    });
                }
                
                numberOfRowsAffected = cmd.ExecuteNonQuery();
            }

            return numberOfRowsAffected;
        }

        public async Task<DataTable> GetDataWithConditions(string query, List<SqlParameter> parameters)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand(query, _dbConnection.Connection))
            {
                cmd.CommandType = CommandType.Text;
                if (parameters != null)
                {
                    parameters.ForEach(parameter =>
                    {
                        cmd.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    });
                }
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
            }

            return dt;
        }
    }
}


