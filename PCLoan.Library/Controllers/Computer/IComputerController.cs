using PCLoan.Logic.Library.Models;

namespace PCLoan.Logic.Library.Controllers
{
    public interface IComputerController
    {
        void CreateLoan(string username, LoanModelDTO model);
        LoanModelDTO GetCurrentLoan(string username);
        LoanModelDTO GetNewLoanModel();
        void ReturnLoan(LoanModelDTO model);
    }
}