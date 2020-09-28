using Dapper.Contrib.Extensions;
using System;

namespace PCLoan.Data.Library.Models
{
    [Table("Log")]
    public class LogModelDAO
    {
        [Key]
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        public int UserId { get; set; }

        public int? ComputerId { get; set; }

        public string Description { get; set; }
    }
}
