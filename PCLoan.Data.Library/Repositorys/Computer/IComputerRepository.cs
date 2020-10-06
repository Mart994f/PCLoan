using PCLoan.Data.Library.Models;
using System.Collections.Generic;

namespace PCLoan.Data.Library.Repositorys
{
    public interface IComputerRepository : IRepository<ComputerModelDAO>
    {
        IEnumerable<ComputerModelDAO> GetAvailableComputers();

        IEnumerable<ComputerModelDAO> GetAllComputersWithCurrentLoan();

        int GetComputerIdByName(string name);

        int DeactivateComputer(int id);
    }
}