using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;

namespace PCLoan.Data.Library
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Private Fields

        private IConfiguration _configuration;

        public string CONNECTION_STRING;

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        public Repository(IConfiguration configuration)
        {
            _configuration = configuration;
            CONNECTION_STRING = _configuration.GetConnectionString("Development");
        }

        #endregion

        #region Public Methods

        public T Get(int id)
        {
            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.Get<T>(1);
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.GetAll<T>();
            }
        }

        public long Insert(T entity)
        {
            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.Insert(entity);
            }
        }

        public bool Update(T entity)
        {
            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.Update(entity);
            }
        }

        public bool Delete(T entity)
        {
            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.Delete<T>(entity);
            }
        }

        #endregion

        #region Private Helper Methods

        #endregion
    }
}
