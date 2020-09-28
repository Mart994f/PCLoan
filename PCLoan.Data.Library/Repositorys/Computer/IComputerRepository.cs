using PCLoan.Data.Library.Models;
using System.Collections.Generic;

namespace PCLoan.Data.Library.Repositorys
{
    public interface IComputerRepository : IRepository<ComputerModelDAO>
    {
        IEnumerable<ComputerModelDAO> GetAvailableComputers();
    }
}