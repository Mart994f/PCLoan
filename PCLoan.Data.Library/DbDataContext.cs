using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace PCLoan.Data.Library
{
    class DbDataContext
    {
        #region Private Fields

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        #endregion

        #region Public Methods

        public IEnumerable<T> GetData<T>(string procedure, object data)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return connection.Query<T>(procedure, data, commandType: CommandType.StoredProcedure, commandTimeout: 10);
            }
        }

        public int SetData<T>(string procedure, object data)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return connection.Execute(procedure, data, commandType: CommandType.StoredProcedure, commandTimeout: 10);
            }
        }

        #endregion

        #region Private Helper Methods

        private string GetConnectionString()
        {
            return "";
        }

        #endregion
    }
}
