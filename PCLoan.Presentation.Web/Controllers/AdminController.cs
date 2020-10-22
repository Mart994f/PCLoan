using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PCLoan.Logic.Library.Controllers;
using PCLoan.Logic.Library.Exceptions;
using PCLoan.Logic.Library.Models;
using PCLoan.Presentation.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PCLoan.Presentation.Web.Controllers
{
    // TODO: Re-enable authorize
    //[Authorize(Roles = Role.EmployeeOrAdministrator)]
    public class AdminController : Controller
    {
        private IComputerController _computerController;

        private IAdminController _adminController;

        private IMapper _mapper;

        public AdminController(IAdminController adminController, IMapper mapper, IComputerController computerController)
        {
            _computerController = computerController;
            _adminController = adminController;
            _mapper = mapper;
        }

        // GET: AdminController
        public ActionResult Index()
        {
            ViewBag.AvailableComputerAmount = _computerController.GetAvailableComputers().Count;
            IEnumerable<ComputerModel> models = _mapper.Map<IEnumerable<ComputerModel>>(_adminController.GetComputersWithLoan());

            return View(models);
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            ComputerModel model = _mapper.Map<ComputerModel>(_adminController.GetComputerWithLoan(id));

            return View(model);
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            ComputerModel model = new ComputerModel();

            model.States = _mapper.Map<List<StateModel>>(_adminController.GetStates());

            var states = from Models.StateModel t in model.States select new SelectListItem { Value = t.Id.ToString(), Text = t.State };
            ViewBag.States = states;

            return View(model);
        }

        // POST: AdminController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(ComputerModel model)
        {
            try
            {
                _adminController.CreateComputer(int.Parse(User.FindFirst("Id").Value), _mapper.Map<ComputerModelDTO>(model));

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            ComputerModel model = _mapper.Map<ComputerModel>(_adminController.GetComputerWithLoan(id));

            var states = from Models.StateModel t in model.States select new SelectListItem { Value = t.Id.ToString(), Text = t.State };
            ViewBag.States = states;

            return View(model);
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ComputerModel model)
        {
            try
            {
                _adminController.UpdateComputer(_mapper.Map<ComputerModelDTO>(model));

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            ComputerModel model = _mapper.Map<ComputerModel>(_adminController.GetComputerWithLoan(id));

            return View(model);
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ComputerModel model)
        {
            try
            {
                _adminController.DeactivateComputer(id);

                return RedirectToAction(nameof(Index));
            }
            catch (CanNotDeleteComputerException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Exception", "Computer", ex);
            }

            return View(model);
        }

        public ActionResult OpenKiosk()
        {
            Response.Cookies.Append("Kiosk", true.ToString());

            return RedirectToAction("Index", "Computer");
        }

        public ActionResult CloseKiosk()
        {
            Response.Cookies.Delete("Kiosk");

            return RedirectToAction("Index");
        }
    }
}
