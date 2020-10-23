using System;
using System.ComponentModel.DataAnnotations;

namespace PCLoan.Presentation.Web.Models
{
    public class LogModel
    {
        public int Id { get; set; }

        [Display(Name = "Dato og tid")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        public DateTime Timestamp { get; set; }

        public int UserId { get; set; }

        [Display(Name = "Bruger")]
        public string Username { get; set; }

        public int? ComputerId { get; set; }

        [Display(Name = "Computernavn")]
        public string Computername { get; set; }

        [Display(Name = "Beskrivelse")]
        public string Description { get; set; }
    }
}
