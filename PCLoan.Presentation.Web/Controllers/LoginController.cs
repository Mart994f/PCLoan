using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PCLoan.Logic.Library.Controllers;
using PCLoan.Logic.Library.Models;
using PCLoan.Presentation.Web.Models;

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
                    Response.Cookies.Append("Auth", model.Token);

                    if (Request.Cookies["Kiosk"] == true.ToString())
                    {
                        return RedirectToAction("Confirm", "Computer");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
                ViewBag.FailedLogin = "Brugernavn eller adgangskode er forkert";
            }

            return View(model);
        }


        public ActionResult Signout()
        {
            Response.Cookies.Delete("Action");
            Response.Cookies.Delete("Auth");

            if (Request.Cookies["Kiosk"] == true.ToString())
                return RedirectToAction("Index", "Computer");

            return RedirectToAction("Login", "Login");
        }
    }
}
