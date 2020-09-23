using Microsoft.Extensions.Logging;
using PCLoan.Logic.Library.Enums;
using PCLoan.Logic.Library.Models;
using System;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Net;

namespace PCLoan.Logic.Library.Services
{
    public class LdapAuthorizationService : IAuthorizationService
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
        /// The logger for logging.
        /// </summary>
        private ILogger<LdapAuthenticationService> _logger;

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        public LdapAuthorizationService(ILogger<LdapAuthenticationService> logger)
        {
            _ldapConnection = new LdapConnection(new LdapDirectoryIdentifier("10.255.1.1", 389));
            _logger = logger;
        }

        #endregion

        #region Public Methods

        #endregion

        #region Internal Methods

        public UserModelDTO AuthorizeUser(UserModelDTO model)
        {
            // Credentials for password-based authentication, used for the LDAP request
            _ldapConnection.Credential = new NetworkCredential(model.UserName, model.Password);

            try
            {
                // Log information about authorizing
                _logger?.LogInformation("Trying to authorize user {user.Username}", model.UserName);

                // Bind to the LDAP server
                _ldapConnection.Bind();

                _principalContext = new PrincipalContext(ContextType.Domain, "10.255.1.1", model.UserName, model.Password);

                // If the user is member of "ZBC-Ansatte(Alle)",
                if (model.UserPrincipal != null && model.UserPrincipal.IsMemberOf(_principalContext, IdentityType.SamAccountName, "ZBC-Ansatte(Alle)"))
                {
                    // then give the user a role of employee
                    model.Role = Role.Employee;
                }
                // Else if the user is member of "zbc_alle_elever",
                else if (model.UserPrincipal != null && model.UserPrincipal.IsMemberOf(_principalContext, IdentityType.SamAccountName, "zbc_alle_elever"))
                {
                    // then give the user a role of student
                    model.Role = Role.Student;
                }

                // Log that the user has been authorized
                _logger?.LogInformation("The user {Username} has been authorized as {Role}", new object[] { model.UserName, model.Role });

                // Return the modified userModel
                return model;
            }
            catch (Exception ex)
            {
                // Log that there has been an error..
                _logger?.LogError(ex, "An exception was caught while trying to authorize user {Username}", model.UserName);

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
