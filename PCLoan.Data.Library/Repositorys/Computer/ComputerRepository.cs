﻿using Dapper;
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
                return connection.Query<ComputerModelDAO>("GetAvailableComputers", null, commandType: CommandType.StoredProcedure, commandTimeout: 10);
            }
        }

        public IEnumerable<ComputerModelDAO> GetAllComputersWithCurrentLoan()
        {
            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.Query<ComputerModelDAO>("GetAllComputersWithCurrentLoan", null, commandType: CommandType.StoredProcedure, commandTimeout: 10);
            }
        }

        #endregion

        #region Private Helper Methods

        #endregion
    }
}
