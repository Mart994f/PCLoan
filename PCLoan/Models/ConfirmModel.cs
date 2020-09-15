using PCLoan.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCLoan.Models
{
    public class ConfirmModel
    {
        public int SelectedComputer { get; set; }
        public List<SelectListItem> AvailableComputers { get; set; }

        public ConfirmModel()
        {
            AvailableComputers = DbDataAccess.GetData<SelectComputerModel>("GetAvailableComputers", null).Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }).ToList();
        }
    }
}