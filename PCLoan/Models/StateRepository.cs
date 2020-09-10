using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PCLoan.Models
{
    public class StateRepository
    {
        public IEnumerable<SelectListItem> GetStates()
        {
            // TODO: Implement getting data from DB
            List<SelectListItem> states = new List<SelectListItem>();

            return new SelectList(states, "Value", "Text");
        }
    }
}
