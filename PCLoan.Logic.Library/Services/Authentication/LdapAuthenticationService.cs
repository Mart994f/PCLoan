using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PCLoan.Logic.Library.Models;
using System;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Net;

namespace PCLoan.Logic.Library.Services
{
    public class LdapAuthenticationService : IAuthenticationService
    {
        #region Private Fields

        /// <summary>
        /// The connection to Microsoft Active Directory Domain Services.
        /// </summary>
        private LdapConnection _ldapConnection;

        /// <summary>
        /// The <see cref="PrincipalContext"/> against which LDAP requests are performed.
        /// </summary>
        private PrincipalContext _principalContext;

        /// <summary>
        /// The <see cref="UserPrincipal"/> found by SamAccountName on the LDAP server.
        /// </summary>
        private UserPrincipal _userPrincipal;

        /// <summary>
        /// The logger for logging.
        /// </summary>
        private ILogger<LdapAuthenticationService> _logger;

        private IConfiguration _configuration;

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        public LdapAuthenticationService(ILogger<LdapAuthenticationService> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _ldapConnection = new LdapConnection(new LdapDirectoryIdentifier(_configuration[_configuration.GetConnectionString("LdapIpAdress")],
                                                                             int.Parse(_configuration[_configuration.GetConnectionString("LdapPort")])));
            _logger = logger;
        }

        #endregion

        #region Public Methods

        public UserModelDTO AuthenticateUser(UserModelDTO model)
        {
            // Credentials for password-based authentication, used for the LDAP request
            _ldapConnection.Credential = new NetworkCredential(model.UserName, model.Password);

            try
            {
                // Log information about authenticate
                _logger?.LogInformation("Trying to authenticate user {Username}", model.UserName);

                // Bind to the LDAP server
                _ldapConnection.Bind();

                _principalContext = new PrincipalContext(ContextType.Domain, _configuration[_configuration.GetConnectionString("LdapIpAdress")], model.UserName, model.Password);

                // Requesting the userPrincipal from the Active Directory Domain Services, searching by SamAccountName
                _userPrincipal = UserPrincipal.FindByIdentity(_principalContext, IdentityType.SamAccountName, model.UserName);

                // If the user exists..
                if (_userPrincipal != null)
                {
                    // set the user to authenticated..
                    model.Authenticated = true;

                    // and save the user principal for authorization
                    model.UserPrincipal = _userPrincipal;

                    // Log that the user has been authenticated
                    _logger?.LogInformation("The user {Username} has been authenticate", model.UserName);
                }

                // Return the modified userModel
                return model;
            }
            catch (Exception ex)
            {
                // Log that there has been an error..
                _logger?.LogError(ex, "An exception was caught while trying to authenticate user {Username}", model.UserName);

                // and return the unmodified userModel
                return model;
            }
            finally
            {
                // Finally dispose the connection to the LDAP server
                _ldapConnection.Dispose();
            }
        }

        #endregion

        #region Private Helper Methods

        #endregion
    }
}
