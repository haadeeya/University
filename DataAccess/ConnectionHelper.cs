using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ConnectionHelper
    {
        public SqlConnection Connection { get; }

        public ConnectionHelper(SqlConnection conn)
        {
            Connection = conn;
        }

        public async Task<SqlTransaction> BeginTransaction()
        {
            await OpenConnection();

            return Connection.BeginTransaction();
        }

        public async Task<DataTable> GetData(string query, List<SqlParameter> parameters = null, SqlTransaction transaction = null)
        {
            if (Connection.State == ConnectionState.Closed)
            {
                await OpenConnection();
            }

            DataTable dataTable = new DataTable();

            using (SqlCommand cmd = new SqlCommand(query, Connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Transaction = transaction;

                if (parameters != null)
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    }
                }

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dataTable);
                }
            }

            return dataTable;
        }

        public async Task<int> UpdateAndInsertData(string query, List<SqlParameter> parameters, SqlTransaction transaction = null)
        {
            if (Connection.State == ConnectionState.Closed)
            {
                await OpenConnection();
            }

            using (SqlCommand command = new SqlCommand(query, Connection))
            {
                command.CommandType = CommandType.Text;
                command.Transaction = (SqlTransaction)transaction;

                if (parameters != null)
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    }
                }

                return command.ExecuteNonQuery();
            }
        }

        private Task OpenConnection()
        {
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }

            return Connection.OpenAsync();
        }
    }
}


