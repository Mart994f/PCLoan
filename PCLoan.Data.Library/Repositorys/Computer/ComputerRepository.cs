using Dapper;
using Microsoft.Extensions.Configuration;
using PCLoan.Data.Library.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PCLoan.Data.Library.Repositorys
{
    public class ComputerRepository : Repository<ComputerModelDAO>, IComputerRepository
    {
        #region Private Fields

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        public ComputerRepository(IConfiguration configuration) : base(configuration)
        {

        }

        #endregion

        #region Public Methods

        public IEnumerable<ComputerModelDAO> GetAvailableComputers()
        {
            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.Query<ComputerModelDAO>("GetAvailableComputers", null,
                                                          commandType: CommandType.StoredProcedure, commandTimeout: 10);
            }
        }

        public IEnumerable<ComputerModelDAO> GetAllComputersWithCurrentLoan()
        {
            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.Query<ComputerModelDAO>("GetAllComputersWithCurrentLoan", null,
                                                          commandType: CommandType.StoredProcedure, commandTimeout: 10);
            }
        }

        public int GetComputerIdByName(string name)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@name", name);

            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.ExecuteScalar<int>("GetComputerIdByName", parameters,
                                                     commandType: CommandType.StoredProcedure, commandTimeout: 10);
            }
        }

        public int DeactivateComputer(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.Execute("DeactivateComputer", parameters, commandType: CommandType.StoredProcedure,
                                          commandTimeout: 10);
            }
        }

        public int UpdateState(int computerId, int stateId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@cimputerId", computerId);
            parameters.Add("@stateId", stateId);

            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.Execute("UpdateState", parameters, commandType: CommandType.StoredProcedure,
                                          commandTimeout: 10);
            }
        }

        #endregion

        #region Private Helper Methods

        #endregion
    }
}
