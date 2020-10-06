using PCLoan.Logic.Library.Models;
using System.Collections.Generic;

namespace PCLoan.Logic.Library.Controllers
{
    public interface IAdminController
    {
        ComputerModelDTO GetComputer(int id);

        ComputerModelDTO GetNewComputerModel();

        IEnumerable<ComputerModelDTO> GetAllComputersWithCurrentLoan();

        void CreateComputer(string username, ComputerModelDTO model, string state);

        void UpdateComputer(ComputerModelDTO model);

        void DeactivateComputer(int id);

        StateModelDTO GetState(int id);
    }
}