using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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

        public async Task<List<Subject>> GetSubjects(string query)
        {
            List<Subject> subjects = new List<Subject>();

            using (SqlCommand cmd = new SqlCommand(query, _dbConnection.Connection))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    subjects = ToList<Subject>(dr);
                }
            }

            return subjects;
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

        public virtual List<T> ToList<T>(IDataReader rdr)
        {
            List<T> ret = new List<T>();
            T entity;
            Type typ = typeof(T);
            PropertyInfo col;
            List<PropertyInfo> columns = new List<PropertyInfo>();
            PropertyInfo[] props = typ.GetProperties();
            for (int index = 0; index < rdr.FieldCount; index++)
            {
                col = props.FirstOrDefault(c => c.Name == rdr.GetName(index));
                if (col != null)
                {
                    columns.Add(col);
                }
            }
            while (rdr.Read())
            {
                entity = Activator.CreateInstance<T>();
                foreach (var column in columns)
                {
                    if (rdr[column.Name].Equals(DBNull.Value))
                    {
                        column.SetValue(entity, null, null);
                    }
                    else
                    {
                        column.SetValue(entity, rdr[column.Name], null);
                    }
                }
                ret.Add(entity);
            }
            return ret;
        }

        Task<DataTable> IDbCommand.GetData(string query)
        {
            throw new NotImplementedException();
        }
    }
}


