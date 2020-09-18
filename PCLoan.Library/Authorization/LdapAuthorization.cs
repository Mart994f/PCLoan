using Microsoft.Extensions.Logging;
using PCLoan.Logic.Library.Enums;
using PCLoan.Logic.Library.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Text;

namespace PCLoan.Logic.Library.Authorization
{
    class LdapAuthorization : IAuthorization
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
        private ILogger<LdapAuthorization> _logger;

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        public LdapAuthorization(ILogger<LdapAuthorization> logger)
        {
            _logger = logger;
            _ldapConnection = new LdapConnection(new LdapDirectoryIdentifier("10.255.1.1", 389));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Authorize a user
        /// </summary>
        /// <param name="user">The <see cref="UserModel"/> to authorize</param>
        /// <returns>An updated <see cref="UserModel"/></returns>
        public UserModel AuthorizeUser(UserModel user)
        {
            // Credentials for password-based authentication, used for the LDAP request
            _ldapConnection.Credential = new NetworkCredential(user.UserName, user.Password);

            try
            {
                // Log information about authorizing
                _logger?.LogInformation("Trying to authorize user {user.Username}", user.UserName);

                // Bind to the LDAP server
                _ldapConnection.Bind();

                _principalContext = new PrincipalContext(ContextType.Domain, "10.255.1.1", user.UserName, user.Password);

                // If the user is member of "ZBC-Ansatte(Alle)",
                if (user.UserPrincipal != null && user.UserPrincipal.IsMemberOf(_principalContext, IdentityType.SamAccountName, "ZBC-Ansatte(Alle)"))
                {
                    // then give the user a role of employee
                    user.Role = Role.Employee;
                }
                // Else if the user is member of "zbc_alle_elever",
                else if (user.UserPrincipal != null && user.UserPrincipal.IsMemberOf(_principalContext, IdentityType.SamAccountName, "zbc_alle_elever"))
                {
                    // then give the user a role of student
                    user.Role = Role.Student;
                }

                // Log that the user has been authorized
                _logger?.LogInformation("The user {user.Username} has been authorized as {user.Role}", user);

                // Return the modified userModel
                return user;
            }
            catch (Exception ex)
            {
                // Log that there has been an error..
                _logger?.LogError(ex, "An exception was caught while trying to authorize user {Username}", user.UserName);

                // and return the unmodified userModel
                return user;
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
