using System.Collections.Generic;

namespace PCLoan.Presentation.Web.Models
{
    public class SearchModel
    {
        public int ComputerId { get; set; }

        public string Username { get; set; }

        public List<ComputerModel> Computers { get; set; }
    }
}
