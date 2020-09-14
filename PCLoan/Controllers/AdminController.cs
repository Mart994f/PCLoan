using PCLoan.Data;
using PCLoan.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PCLoan.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            List<ComputerModel> computers = DbDataAccess.GetData<ComputerModel>("GetAllComputers", null).ToList();

            return View(computers);
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            List<ComputerModel> computers = DbDataAccess.GetData<ComputerModel>("GetAllComputers", null).ToList();

            ComputerModel computer = computers.Where(c => c.Id == id).First();

            return View(computer);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            ComputerModel computer = new ComputerModel();

            return View(computer);
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            List<ComputerModel> computers = DbDataAccess.GetData<ComputerModel>("GetAllComputers", null).ToList();

            ComputerModel computer = computers.Where(c => c.Id == id).First();

            return View(computer);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            List<ComputerModel> computers = DbDataAccess.GetData<ComputerModel>("GetAllComputers", null).ToList();

            ComputerModel computer = computers.Where(c => c.Id == id).First();

            return View(computer);
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
