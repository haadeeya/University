using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Interface
{
    public interface IDBCommand
    {
        DataTable GetData(string query);
        int UpdateAndInsertData(string query, List<SqlParameter> parameters);
        DataTable GetDataWithConditions(string query, List<SqlParameter> parameters);
    }
}
