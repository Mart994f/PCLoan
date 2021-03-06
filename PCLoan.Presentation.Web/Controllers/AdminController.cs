﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PCLoan.Logic.Library.Controllers;
using PCLoan.Presentation.Web.Models;
using System.Collections.Generic;

namespace PCLoan.Presentation.Web.Controllers
{
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(ComputerModel model)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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
        public ActionResult Edit(int id, ComputerModel model)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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
        public ActionResult Delete(int id, ComputerModel model)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
