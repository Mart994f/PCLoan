using PCLoan.Data;
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

        // GET: Computer/Confirm
        public ActionResult Confirm()
        {
            ConfirmModel model = new ConfirmModel();
            string action = Request.Cookies["action"].Value;
            if (action == "loan")
            {
                ViewBag.DropdownMessage = "Vælg udleveret PC";
            }
            else if (action == "return")
            {
                ViewBag.DropdownMessage = "Indlever din PC";
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Confirm(ConfirmModel model)
        {
            List<ConfirmModel> computers = DbDataAccess.GetData<ConfirmModel>("GetAvailableComputers", null).ToList();


            return View(model);
        }

        // GET: Login/Login


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