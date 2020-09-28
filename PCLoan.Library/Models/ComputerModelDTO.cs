using System;
using System.Collections.Generic;

namespace PCLoan.Logic.Library.Models
{
    public class ComputerModelDTO
    {
        public int Id { get;  set; }

        public string Name { get; set; }

        public int StateId { get; set; }

        public List<StateModelDTO> States { get; set; }

        public string LendBy { get; set; }

        public DateTime LoanDate { get; set; }

        public DateTime? ReturnedDate { get; set; }

        public bool Deactivated { get; set; }
    }
}
