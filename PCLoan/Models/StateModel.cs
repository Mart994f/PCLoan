using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCLoan.Models
{
    public class StateModel
    {
        public int Id { get; set; }

        public string State { get; set; }
        public List<Computer> GetAllComputers()
        {
            return null;
        }
    }
}