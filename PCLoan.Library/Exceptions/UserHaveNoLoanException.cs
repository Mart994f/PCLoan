using System;

namespace PCLoan.Logic.Library.Exceptions
{
    public class UserHaveNoLoanException : Exception
    {
        public UserHaveNoLoanException(string message) : base(message)
        {

        }
    }
}
