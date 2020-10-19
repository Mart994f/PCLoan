using PCLoan.Data.Library.Models;
using System;

namespace PCLoan.Data.Library.Repositorys
{
    public interface ILoanRepository : IRepository<LoanModelDAO>
    {
        bool CheckUserHaveLoan(int userId);
        int ReturnLoan(int loanId, DateTime returnDate);
    }
}