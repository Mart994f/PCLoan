using PCLoan.Logic.Library.Models;
using System.Collections.Generic;

namespace PCLoan.Logic.Library.Controllers
{
    public interface IComputerController
    {
        List<ComputerModelDTO> GetAvailableComputers();
        LoanModelDTO GetUsersCurrentLoan(int userID);
        void RegisterLoan(int userId, LoanModelDTO model);
        void RegisterLoanReturned(int userId);
    }
}