using System;

namespace PCLoan.Presentation.Web.Models
{
    public class LogModel
    {
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        public int UserId { get; set; }

        public int? ComputerId { get; set; }

        public string Description { get; set; }
    }
}
