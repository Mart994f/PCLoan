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
            IEnumerable<SelectListItem> states = DbDataAccess.GetData<StateModel>("GetStates", null)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.State
                });

            List<ComputerModel> computers = DbDataAccess.GetData<ComputerModel>("GetAllComputers", null).ToList();
            computers.ForEach(c => c.states = states);
            computers.Where(c => c.states. = c.SelectedState)

            return View(computers);
        }
    }
}