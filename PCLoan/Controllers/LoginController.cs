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
        public ActionResult Login(LoginModel model)
        {
            bool isValid = model.ValidateUser();
            if (isValid)
            {
                model.GetInformation();

                string requestName = Request.Cookies["action"].Value;
                if (requestName == "loan")
                {
                    return RedirectToAction("Confirm", "Computer");
                }
                else if (requestName == "return")
                {
                    return RedirectToAction("Confirm", "Computer");
                }
            }
            ViewBag.FailedLogin = "Brugernavn eller adgangskode er forkert";
            return View();
        }
    }
}
