using System;

namespace PCLoan.Logic.Library.Models
{
    public class LoanModelDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ComputerId { get; set; }

        public DateTime LoanDate { get; set; }

        public DateTime ReturnedDate { get; set; }
    }
}
