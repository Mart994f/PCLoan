using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCLoan.Models
{
    public class Computer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        
        public string SelectedState { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }

        [Display(Name = "Lånt af:")]
        public string LendBy { get; set; }
    }
}