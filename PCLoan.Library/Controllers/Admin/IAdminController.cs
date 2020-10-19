using PCLoan.Logic.Library.Models;
using System.Collections.Generic;

namespace PCLoan.Logic.Library.Controllers
{
    public interface IAdminController
    {
        void CreateComputer(int userId, ComputerModelDTO model);
        void DeactivateComputer(int id);
        List<ComputerModelDTO> GetComputersWithLoan();
        ComputerModelDTO GetComputerWithLoan(int id);
        void UpdateComputer(ComputerModelDTO model);
        List<StateModelDTO> GetStates();
    }
}