using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PCLoan.Logic.Library.Controllers;
using PCLoan.Logic.Library.Models;
using PCLoan.Presentation.Web.Models;
using System.Net;

namespace PCLoan.Presentation.Web.Controllers
{
    public class LoginController : Controller
    {
        private ILoginController _loginController;

        private IMapper _mapper;

        public LoginController(ILoginController loginController, IMapper mapper)
        {
            _loginController = loginController;
            _mapper = mapper;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel model)
        {
            if (ModelState.IsValid)
            {
                model = _mapper.Map<UserModel>(_loginController.LoginUser(_mapper.Map<UserModelDTO>(model)));
                if (model.Authenticated)
                {
                    return Json(model);
                    //return RedirectToAction("Confirm", "Computer");
                }
            }

            return View(model);
        }

        public ActionResult Signout()
        {
            ViewBag.LogoutMessage = "Du vil nu blive logget ud";
            if (Request.Cookies["action"] == "loanPc")
            {
                ViewBag.SignoutMessage = "Du har nu udlånt en pc";
            }
            else if (Request.Cookies["action"] == "returnPc")
            {
                ViewBag.SignoutMessage = "Du har nu afleveret din pc";
            }
            return RedirectToAction("Index", "Computer");
        }
    }
}
