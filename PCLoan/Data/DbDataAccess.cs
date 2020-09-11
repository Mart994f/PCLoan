using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PCLoan.Data
{
    public static class DbDataAccess
    {
        public static string GetConnectionString(string conectionName = "PcLoan")
        {
            return ConfigurationManager.ConnectionStrings[conectionName].ConnectionString;
        }

        public static IEnumerable<T> GetData<T>(string procedure, object data)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString("Development")))
            {
                return connection.Query<T>(procedure, data, commandType: CommandType.StoredProcedure, commandTimeout: 10);
            }
        }

        public static int SetData<T>(string procedure, T data)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString("Development")))
            {
                return connection.Execute(procedure, data, commandType: CommandType.StoredProcedure, commandTimeout: 10);
            }
        }
    }
}