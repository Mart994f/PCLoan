using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PCLoan.Models
{
    public class ComputerModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Status")]
        public string SelectedState { get; set; }
        public IEnumerable<SelectListItem> states { get; set; }

        [Display(Name = "Udlånt til:")]
        public string LendBy { get; set; }

        [Display(Name = "Lånt:")]
        public DateTime LoanDate { get; set; }

        [Display(Name = "Afleværes:")]
        public DateTime ReturnDate { get; set; }
    }
}