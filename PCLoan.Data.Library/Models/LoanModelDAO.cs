using Dapper.Contrib.Extensions;
using System;

namespace PCLoan.Data.Library.Models
{
    [Table("Loan")]
    public class LoanModelDAO
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ComputerId { get; set; }

        public DateTime LoanDate { get; set; }

        public DateTime? ReturnedDate { get; set; } = null;
    }
}
