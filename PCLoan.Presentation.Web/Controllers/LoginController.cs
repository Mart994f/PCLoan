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
                }
            }

            return View(model);
        }

        public ActionResult Signout()
        {
            return View();
        }
    }
}
