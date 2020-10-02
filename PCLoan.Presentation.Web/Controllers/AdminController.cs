using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCLoan.Logic.Library.Controllers;
using PCLoan.Logic.Library.Models;
using PCLoan.Presentation.Web.Enums;
using PCLoan.Presentation.Web.Models;
using System;
using System.Collections.Generic;

namespace PCLoan.Presentation.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IAdminController _adminController;

        private IMapper _mapper;

        public AdminController(IAdminController adminController, IMapper mapper)
        {
            _adminController = adminController;
            _mapper = mapper;
        }

        // GET: AdminController
        public ActionResult Index()
        {
            IEnumerable<ComputerModel> models = _mapper.Map<IEnumerable<ComputerModel>>(_adminController.GetAllComputersWithCurrentLoan());

            return View(models);
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            ComputerModel model = _mapper.Map<ComputerModel>(_adminController.GetComputer(id));

            return View(model);
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            ComputerModel model = _mapper.Map<ComputerModel>(_adminController.GetNewComputerModel());

            return View(model);
        }

        // POST: AdminController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(ComputerModel model)
        {
            try
            {
                _adminController.CreateComputer(User.FindFirst("Username").Value, _mapper.Map<ComputerModelDTO>(model), _mapper.Map<StateModel>(_adminController.GetState(model.StateId)).State);

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
            ComputerModel model = _mapper.Map<ComputerModel>(_adminController.GetComputer(id));

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
            ComputerModel model = _mapper.Map<ComputerModel>(_adminController.GetComputer(id));

            return View(model);
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ComputerModel model)
        {
            model.Deactivated = true;

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
    }
}
