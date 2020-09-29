using Microsoft.Extensions.Configuration;
using PCLoan.Data.Library.Models;

namespace PCLoan.Data.Library.Repositorys
{
    public class LogRepository : Repository<LogModelDAO>, ILogRepository
    {
        #region Private Fields

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        public LogRepository(IConfiguration configuration) : base(configuration)
        {

        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Helper Methods

        #endregion
    }
}
