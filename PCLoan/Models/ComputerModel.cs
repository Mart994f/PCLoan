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

        [DataType(DataType.Date)]
        [Display(Name = "Udlånt")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? LoanDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Afleværet")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? ReturnDate { get; set; }

        public ComputerModel()
        {
            States = DbDataAccess.GetData<StateModel>("GetAllStates", null).Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.State }).ToList();
        }
    }
}