using System;

namespace PCLoan.Presentation.Web.Models
{
    public class LoanModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ComputerId { get; set; }

        public DateTime LoanDate { get; set; }

        public DateTime ReturnedDate { get; set; }
    }
}
