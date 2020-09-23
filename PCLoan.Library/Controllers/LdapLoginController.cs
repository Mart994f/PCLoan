using Microsoft.Extensions.Logging;
using PCLoan.Logic.Library.Models;
using PCLoan.Logic.Library.Services;

namespace PCLoan.Logic.Library.Controllers
{
    public class LdapLoginController : ILoginController
    {
        #region Private Fields

        private IAuthenticationService _authenticationService;

        private IAuthorizationService _authorizationService;

        private ILogger<LdapLoginController> _logger;

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        public LdapLoginController(IAuthenticationService authenticationService, IAuthorizationService authorizationService, ILogger<LdapLoginController> logger)
        {
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
            _logger = logger;
        }

        #endregion

        #region Public Methods

        public UserModelDTO LoginUser(UserModelDTO model)
        {
            model = _authenticationService.AuthenticateUser(model);

            if (!model.Authenticated)
                return model;

            model = _authorizationService.AuthorizeUser(model);

            model.Password = string.Empty;

            return model;
        }

        #endregion

        #region Private Helper Methods

        #endregion
    }
}
