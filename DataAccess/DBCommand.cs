using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using Interface;
using System.Threading.Tasks;
using IDbCommand = Interface.IDbCommand;

namespace DataAccess
{
    public class DBCommand : IDbCommand
    {
        private readonly IDBConnection _dbConnection;

        public DBCommand()

        {

            _dbConnection = new DBConnection();

        }

        public DataTable GetData(string query)

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

             _dbConnection.CloseConnection();

            return dt;

        }

        public int UpdateAndInsertData(string query, List<SqlParameter> parameters)

        {

            int numberOfRowsAffected = 0;



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

                numberOfRowsAffected = cmd.ExecuteNonQuery();

            }



            _dbConnection.CloseConnection();

            return numberOfRowsAffected;

        }

        public DataTable GetDataWithConditions(string query, List<SqlParameter> parameters)

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

             _dbConnection.CloseConnection();

            return dt;

        }
    }
}
