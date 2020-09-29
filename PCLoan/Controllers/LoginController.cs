using PCLoan.Data;
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

        // POST: Login
        [HttpPost]
        public ActionResult Login(LoginModel model, string name)
        {
            bool isValid = model.ValidateUser();


            if (isValid)
            {
                Response.Cookies["username"].Value = model.username.ToString();
                //model.username = DbDataAccess.GetData<LoginModel>("CheckUserExists", parameters).Select(s => new SelectListItem { Value = s.username, Text = s.username });

                Dapper.DynamicParameters parameters = new Dapper.DynamicParameters();
                parameters.Add(@"name", Request.Cookies["username"].Value);

                List<LoginModel> users = DbDataAccess.GetData<LoginModel>("CheckUserExists", parameters).ToList();

                if (users[0] == null)
                {
                    DbDataAccess.SetData("AddUser", parameters);
                }

                model.GetInformation();
                ViewBag.FailedLogin = null;
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
