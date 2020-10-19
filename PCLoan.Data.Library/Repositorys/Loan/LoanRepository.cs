using Dapper;
using Microsoft.Extensions.Configuration;
using PCLoan.Data.Library.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PCLoan.Data.Library.Repositorys
{
    public class LoanRepository : Repository<LoanModelDAO>, ILoanRepository
    {
        #region Private Fields

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        public LoanRepository(IConfiguration configuration) : base(configuration)
        {

        }

        #endregion

        #region Public Methods

        public bool CheckUserHaveLoan(int userId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@userId", userId);

            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.ExecuteScalar<bool>("CheckUserHaveLoan", parameters, commandType: CommandType.StoredProcedure,
                                          commandTimeout: 10);
            }
        }

        public int ReturnLoan(int loanId, DateTime returnDate)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@loanId", loanId);
            parameters.Add("@returnDate", returnDate);


            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.Execute("ReturnLoan", parameters, commandType: CommandType.StoredProcedure,
                                          commandTimeout: 10);
            }
        }

        #endregion

        #region Private Helper Methods

        #endregion
    }
}
