using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        public IActionResult Index(string loanPc)
        {
            loanPc = Request.Cookies["action"];
            Response.Cookies.Append("action", loanPc);

            ViewBag.LogoutMessage = "Du vil nu blive logget ud";
            ViewBag.SignoutMessage = "Du har nu udlånt en pc";
            ViewBag.SignoutMessage = "Du har nu afleveret din pc";

            return RedirectToAction("Login", "Login");
        }

        public IActionResult Confirm()
        {
            LoanModel model = null;
            //If a computer is lent
            if (Request.Cookies["action"] == "loanPc")
            {
                model = _mapper.Map<LoanModel>(_computerController.GetNewLoanModel());
            }
            //If a computer is returned
            else if (Request.Cookies["action"] == "returnPc")
            {
                // TODO: Implement username from the Json WebToken
                model = _mapper.Map<LoanModel>(_computerController.GetCurrentLoan(""));
            }
            return View();
        }

        [HttpPost]
        public IActionResult Confirm(LoanModel model)
        {
            //For lending a computer
            if (Request.Cookies["action"] == "loanPc")
            {
                // TODO: Get the right username via Json WebToken
                _computerController.CreateLoan(null, _mapper.Map<LoanModelDTO>(model));
            }
            //For returning a computer
            else if (Request.Cookies["action"] == "returnPc")
            {
                _computerController.ReturnLoan(_mapper.Map<LoanModelDTO>(model));
            }
            return View(model);
        }
    }
}
