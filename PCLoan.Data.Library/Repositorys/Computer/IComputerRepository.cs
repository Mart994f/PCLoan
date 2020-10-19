using PCLoan.Data.Library.Models;
using System.Collections.Generic;

namespace PCLoan.Data.Library.Repositorys
{
    public interface IComputerRepository : IRepository<ComputerModelDAO>
    {
        int DeactivateComputer(int id);
        IEnumerable<ComputerModelDAO> GetAllComputersWithCurrentLoan();
        IEnumerable<ComputerModelDAO> GetAvailableComputers();
        int GetComputerIdByName(string name);
        int UpdateState(int computerId, int stateId);
    }
}