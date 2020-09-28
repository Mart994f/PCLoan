using PCLoan.Logic.Library.Models;
using System.Collections.Generic;

namespace PCLoan.Logic.Library.Controllers
{
    public interface IAdminController
    {
        ComputerModelDTO GetComputer(int id);

        ComputerModelDTO GetNewComputerModel();

        IEnumerable<ComputerModelDTO> GetAllComputersWithCurrentLoan();
    }
}