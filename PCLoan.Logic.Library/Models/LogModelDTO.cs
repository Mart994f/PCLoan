using System;
using System.Collections.Generic;

namespace PCLoan.Logic.Library.Models
{
    public class LogModelDTO
    {
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }

        public int? ComputerId { get; set; }

        public string Computername { get; set; }

        public string Description { get; set; }
    }
}
