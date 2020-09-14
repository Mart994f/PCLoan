using PCLoan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace PCLoan.Models
{
    public class ComputerModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Navn")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int SelectedState { get; set; }
        public List<SelectListItem> States { get; set; }

        [Display(Name = "Udlånt til")]
        public string LendBy { get; set; }

        [Display(Name = "Udlånt")]
        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime? LoanDate { get; set; }

        [Display(Name = "Afleværes")]
        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime? ReturnDate { get; set; }

        public ComputerModel()
        {
            States = DbDataAccess.GetData<StateModel>("GetStates", null).Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.State }).ToList();
        }
    }
}