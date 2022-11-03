using Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
namespace DataAccess
{
    public class DBCommand : IDbCommandSubject, IDbCommandStudent
    {
        private readonly IDBConnection _dbConnection;

        public IDbConnection Connection => _dbConnection.Connection;

        public DBCommand()
        {
            _dbConnection = new DBConnection();
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

        public async Task<IEnumerable<Subject>> GetData(string query)
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

            return await Task.FromResult(subjects.ToList());
        }

       public async Task<Subject> GetDataWithConditions(string query, List<SqlParameter> parameters)
        {
            Subject subject = new Subject();

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
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    subject = MapSubject<Subject>(dr);
                }
            }
            return await Task.FromResult(subject);
        }

        private T MapSubject<T>(SqlDataReader rdr)
        {
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
            entity = Activator.CreateInstance<T>();
            while (rdr.Read())
            {
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
            }
            return entity;
        }

        public Task<List<Subject>> GetAll(string query)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Subject>> IDbCommand<Subject>.GetData(string query)
        {
            throw new NotImplementedException();
        }

        Task<Subject> IDbCommand<Subject>.GetDataWithConditions(string query, List<SqlParameter> parameters)
        {
            throw new NotImplementedException();
        }

        Task<DataTable> IDbCommandStudent.GetData(string query)
        {
            throw new NotImplementedException();
        }

        Task<DataTable> IDbCommandStudent.GetAll(string query)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Student>> IDbCommand<Student>.GetData(string query)
        {
            throw new NotImplementedException();
        }

        Task<Student> IDbCommand<Student>.GetDataWithConditions(string query, List<SqlParameter> parameters)
        {
            throw new NotImplementedException();
        }

        Task<List<Student>> IDbCommand<Student>.GetAll(string query)
        {
            throw new NotImplementedException();
        }

        Task<DataTable> IDbCommandStudent.GetDataWithConditions(string query, List<SqlParameter> parameters)
        {
            throw new NotImplementedException();
        }
    }
}


