using PCLoan.Logic.Library.Models;
using System.Collections.Generic;

namespace PCLoan.Logic.Library.Controllers
{
    public interface IAdminController
    {
        IEnumerable<ComputerModelDTO> GetAllComputersWithCurrentLoan();
    }
}