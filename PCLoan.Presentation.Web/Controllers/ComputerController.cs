using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PCLoan.Logic.Library.Controllers;
using PCLoan.Logic.Library.Exceptions;
using PCLoan.Logic.Library.Models;
using PCLoan.Presentation.Web.Models;
using System.Collections.Generic;

namespace PCLoan.Presentation.Web.Controllers
{
    public class ComputerController : Controller
    {
        private IMapper _mapper;

        private IComputerController _computerController;

        public ComputerController(IMapper mapper, IComputerController computerController)
        {
            _mapper = mapper;
            _computerController = computerController;
        }

        public IActionResult Index()
        {
            ViewBag.AvailableComputerAmount = _computerController.GetAvailableComputers().Count;
            return View();
        }

        [HttpPost]
        public IActionResult Index(string submitButton)
        {
            if (submitButton == "loan" || submitButton == "return")
            {
                Response.Cookies.Append("Action", submitButton);
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        public IActionResult Confirm()
        {
            LoanModel model = null;
            string action = Request.Cookies["Action"];
            //If a computer is lent
            if (action == "loan")
            {
                model = new LoanModel();

                ViewBag.DropdownButton = "Lån";
                model.Computers = _mapper.Map<List<ComputerModel>>(_computerController.GetAvailableComputers());
            }
            //If a computer is returned
            else if (action == "return")
            {
                ViewBag.DropdownButton = "Aflever";
                try
                {
                    model = _mapper.Map<LoanModel>(_computerController.GetUsersCurrentLoan(int.Parse(User.FindFirst("Id").Value)));
                }
                catch (UserHaveNoLoanException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }              
            }
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Confirm(LoanModel model)
        {
            //For lending a computer
            if (Request.Cookies["action"] == "loan")
            {
                _computerController.RegisterLoan(int.Parse(User.FindFirst("Id").Value), _mapper.Map<LoanModelDTO>(model));
                return RedirectToAction("Signout", "Computer");
            }
            //For returning a computer
            else if (Request.Cookies["action"] == "return")
            {
                _computerController.RegisterLoanReturned(int.Parse(User.FindFirst("Id").Value));
                return RedirectToAction("Signout", "Computer");
            }
            return View(model);
        }

        public IActionResult Signout()
        {
            ViewBag.LogoutMessage = "Du vil nu blive logget ud";

            if (Request.Cookies["Action"] == "loan")
            {
                ViewBag.SignoutMessage = "Du har nu lånt en pc";
            }
            else if (Request.Cookies["Action"]  == "return")
            {
                ViewBag.SignoutMessage = "Du har nu afleveret din pc";
            }

            return View();
        }
    }
}
