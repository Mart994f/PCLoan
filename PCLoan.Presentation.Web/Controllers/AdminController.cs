using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCLoan.Logic.Library.Controllers;
using PCLoan.Logic.Library.Exceptions;
using PCLoan.Logic.Library.Models;
using PCLoan.Logic.Library.Values;
using PCLoan.Presentation.Web.Models;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace PCLoan.Presentation.Web.Controllers
{
    // TODO: Re-enable authorize
    //[Authorize(Roles = Role.Administrator)]
    public class AdminController : Controller
    {
        private IAdminController _adminController;

        private IMapper _mapper;

        private IComputerController _computerController;

        private ILogController _logController;

        public AdminController(IAdminController adminController, IMapper mapper, IComputerController computerController, ILogController logController)
        {
            _computerController = computerController;
            _logController = logController;
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
            model.States.Remove(model.States.Find(s => s.Id == (int)State.Lend));

            return View(model);
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ComputerModel model)
        {
            try
            {
                _adminController.CreateComputer(int.Parse(User.FindFirst("Id").Value), _mapper.Map<ComputerModelDTO>(model));

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                model.States = _mapper.Map<List<StateModel>>(_adminController.GetStates());

                return View(model);
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            ComputerModel model = _mapper.Map<ComputerModel>(_adminController.GetComputerWithLoan(id));
            model.States.Remove(model.States.Find(s => s.Id == (int)State.Lend));

            return View(model);
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ComputerModel model)
        {
            try
            {
                _adminController.UpdateComputer(int.Parse(User.FindFirst("Id").Value), _mapper.Map<ComputerModelDTO>(model));

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                model.States = _mapper.Map<List<StateModel>>(_adminController.GetStates());

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
        public ActionResult Delete(int computerId, ComputerModel model)
        {
            try
            {
                _adminController.DeactivateComputer(int.Parse(User.FindFirst("Id").Value), computerId);

                return RedirectToAction(nameof(Index));
            }
            catch (CanNotDeleteComputerException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Exception", "Computer", new { errorMessage = ex.Message });
            }
        }

        public ActionResult Log()
        {
            Tuple<List<LogModel>, SearchModel> tuple;
            SearchModel searchModel = new SearchModel { Computers = _mapper.Map<List<ComputerModel>>(_adminController.GetComputersWithLoan()) };

            tuple = new Tuple<List<LogModel>, SearchModel>(_mapper.Map<List<LogModel>>(_logController.GetLogs()), searchModel);

            return View("Log", tuple);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchUser(string username)
        {
            List<LogModel> logs;
            Tuple<List<LogModel>, SearchModel> tuple;
            SearchModel searchModel = new SearchModel { Computers = _mapper.Map<List<ComputerModel>>(_adminController.GetComputersWithLoan()) };

            if (string.IsNullOrEmpty(username))
            {
                logs = _mapper.Map<List<LogModel>>(_logController.GetLogs());
            }
            else
            {
                logs = _mapper.Map<List<LogModel>>(_logController.GetLogByUsername(username));
            }

            tuple = new Tuple<List<LogModel>, SearchModel>(logs, searchModel);

            return View("Log", tuple);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchComputer(int computerId)
        {
            List<LogModel> logs;
            Tuple<List<LogModel>, SearchModel> tuple;
            SearchModel searchModel = new SearchModel { Computers = _mapper.Map<List<ComputerModel>>(_adminController.GetComputersWithLoan()) };

            if (computerId <= 0)
            {
                logs = _mapper.Map<List<LogModel>>(_logController.GetLogs());
            }
            else
            {
                logs = _mapper.Map<List<LogModel>>(_logController.GetLogByComputerId(computerId));
            }

            tuple = new Tuple<List<LogModel>, SearchModel>(logs, searchModel);

            return View("Log", tuple);
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
