using PCLoan.Logic.Library.Models;
using System.Collections.Generic;

namespace PCLoan.Logic.Library.Controllers
{
    public interface IAdminController
    {
        void CreateComputer(int userId, ComputerModelDTO model);
        void DeactivateComputer(int userId, int computerId);
        List<ComputerModelDTO> GetComputersWithLoan();
        ComputerModelDTO GetComputerWithLoan(int id);
        List<StateModelDTO> GetStates();
        void UpdateComputer(int userId, ComputerModelDTO model);
    }
}