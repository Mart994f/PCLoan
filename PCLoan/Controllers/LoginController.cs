using PCLoan.Models;
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
            PCLoan.Models.LoginModel login = new LoginModel();
            login.GetInformation(username);
            bool isValid = login.ValidateUser(username, password);
            if (true)
            {

            }
            return View();
        }
    }
}
