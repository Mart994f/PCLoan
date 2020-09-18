﻿using Microsoft.Extensions.Logging;
using PCLoan.Logic.Library.Models;
using System;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Net;

namespace PCLoan.Logic.Library.Authentication
{
    /// <summary>
    /// Authenticates a user against Microsoft Active Directory Domain Services with LDAP request.
    /// </summary>
    public class LdapAuthentication : IAuthentication
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
        private ILogger<LdapAuthentication> _logger;

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        public LdapAuthentication(ILogger<LdapAuthentication> logger)
        {
            _logger = logger;
            _ldapConnection = new LdapConnection(new LdapDirectoryIdentifier("10.255.1.1", 389));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Authenticate a user.
        /// </summary>
        /// <param name="user">The <see cref="UserModel"/> to authenticate</param>
        /// <returns>An updated <see cref="UserModel"/></returns>
        public UserModel AuthenticateUser(UserModel user)
        {
            // Credentials for password-based authentication, used for the LDAP request
            _ldapConnection.Credential = new NetworkCredential(user.UserName, user.Password);

            try
            {
                // Log information about authenticate
                _logger?.LogInformation("Trying to authenticate user {Username}", user.UserName);

                // Bind to the LDAP server
                _ldapConnection.Bind();

                _principalContext = new PrincipalContext(ContextType.Domain, "10.255.1.1", user.UserName, user.Password);

                // Requesting the userPrincipal from the Active Directory Domain Services, searching by SamAccountName
                _userPrincipal = UserPrincipal.FindByIdentity(_principalContext, IdentityType.SamAccountName, user.UserName);

                // If the user exists..
                if (_userPrincipal != null)
                {
                    // set the user to authenticated..
                    user.Authenticated = true;

                    // and save the user principal for authorization
                    user.UserPrincipal = _userPrincipal;

                    // Log that the user has been authenticated
                    _logger?.LogInformation("The user {Username} has been authenticate", user.UserName);
                }

                // Return the modified userModel
                return user;
            }
            catch (Exception ex)
            {
                // Log that there has been an error..
                _logger?.LogError(ex, "An exception was caught while trying to authenticate user {Username}", user.UserName);

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
