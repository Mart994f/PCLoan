using Dapper;
using Microsoft.Extensions.Configuration;
using PCLoan.Data.Library.Models;
using System.Data;
using System.Data.SqlClient;

namespace PCLoan.Data.Library
{
    public class UserRepository : Repository<UserModelDAO>, IUserRepository
    {
        #region Private Fields

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        public UserRepository(IConfiguration configuration) : base(configuration)
        {

        }

        #endregion

        #region Public Methods

        public int GetIdByname(string name)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@name", name);

            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.ExecuteScalar<int>("GetUserIdByName", parameters, commandType: CommandType.StoredProcedure, commandTimeout: 10);
            }
        }


        public bool Exsist(string name)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@name", name);

            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.ExecuteScalar<bool>("UserExist", parameters, commandType: CommandType.StoredProcedure, commandTimeout: 10);
            }
        }

        #endregion

        #region Private Helper Methods

        #endregion

    }
}
