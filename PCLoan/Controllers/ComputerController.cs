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
                ViewBag.DropdownMessage = "Vælg en computer";
                ViewBag.DropdownButton = "Udlån";
                model.AvailableComputers = DbDataAccess.GetData<SelectComputerModel>("GetAvailableComputers", null).Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }).ToList();
            }
            else if (action == "return")
            {
                ViewBag.DropdownMessage = "Indlever din PC";
                ViewBag.DropdownButtong = "Aflever";
                Dapper.DynamicParameters parameters = new Dapper.DynamicParameters();
                parameters.Add("@username", Request.Cookies["username"].Value);
                model.AvailableComputers = DbDataAccess.GetData<SelectComputerModel>("GetLentComputer", parameters).Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }).ToList();
            }
            return View(model);
        }

        /*
        [HttpPost]
        public ActionResult Confirm()
        {

        }
        */

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