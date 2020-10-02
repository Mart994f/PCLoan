using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCLoan.Logic.Library.Controllers;
using PCLoan.Logic.Library.Models;
using PCLoan.Presentation.Web.Models;

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

                model = _mapper.Map<LoanModel>(_computerController.GetNewLoanModel());
                return RedirectToAction("Signout", "Login");
            }
            //If a computer is returned
            else if (action == "return")
            {
                // TODO: Implement username from the Json WebToken
                model = _mapper.Map<LoanModel>(_computerController.GetCurrentLoan("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"));
                return RedirectToAction("Signout", "Login");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Confirm(LoanModel model)
        {
            //For lending a computer
            if (Request.Cookies["action"] == "loan")
            {

                // TODO: Get the right username via Json WebToken
                _computerController.CreateLoan(null, _mapper.Map<LoanModelDTO>(model));
                return RedirectToAction("Signout", "Login");
            }
            //For returning a computer
            else if (Request.Cookies["action"] == "return")
            {
                _computerController.ReturnLoan(_mapper.Map<LoanModelDTO>(model));
                return RedirectToAction("Signout", "Login");
            }
            return View(model);
        }
    }
}
