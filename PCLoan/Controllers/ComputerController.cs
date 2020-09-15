using PCLoan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCLoan.Controllers
{
    public class ComputerController : Controller
    {
        // GET: Computer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Confirm()
        {
            return View();
        }
        public ActionResult RedirectToLogin(string loanPc, string returnPc)
        {
            if (loanPc == "loanPc")
            {
                Response.Cookies["action"].Value = "loan";
            }
            else if (returnPc == "returnPc")
            {
                Response.Cookies["action"].Value = "return";
            }

            Response.Cookies["action"].Expires.AddMinutes(10);
            return RedirectToAction("Login", "Login");
        }
    }
}