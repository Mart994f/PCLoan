using Microsoft.Extensions.Configuration;
using PCLoan.Data.Library.Models;

namespace PCLoan.Data.Library.Repositorys
{
    public class StateRepository : Repository<StateModelDAO>, IStateRepository
    {
        #region Private Fields

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        public StateRepository(IConfiguration configuration) : base(configuration)
        {

        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Helper Methods

        #endregion
    }
}
