using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PCLoan.Presentation.Web.Models
{
    public class ComputerModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Navn")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int StateId { get; set; }

        public List<StateModel> States { get; set; }

        [Display(Name = "Udlånt til")]
        public string LendBy { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Udlånt")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? LoanDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Afleværet")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? ReturnedDate { get; set; }

        public bool Deactivated { get; set; }
    }
}
