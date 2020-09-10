using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCLoan.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            bool ValidateUser()
            {
                bool _valid = false;

                LdapDirectoryIdentifier identifier = new LdapDirectoryIdentifier("10.255.1.1", 389);
                LdapConnection connection = new LdapConnection(identifier)
                {
                    Credential = new System.Net.NetworkCredential(username, password)
                };

                try
                {
                    connection.Bind();
                    PrincipalContext context = new PrincipalContext(ContextType.Domain, "10.255.1.1", username, password);
                    UserPrincipal principal = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, username);
                    _valid = true;
                }

                catch
                {

                }

                return _valid;
            }

            string GetInformation()
            {
                string _fullName = null;
                // set up domain context using the default domain you're currently logged in 
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                {
                    // find a user
                    UserPrincipal user = UserPrincipal.FindByIdentity(context, username);

                    if (user != null)
                    {
                        // get the "DisplayName" property ("Fullname" is WinNT specific)
                        _fullName = user.DisplayName;

                        // do something here....        
                    }
                }

                return _fullName;
            }
            return View();
        }
    }
}
