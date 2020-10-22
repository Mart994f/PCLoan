using System;

namespace PCLoan.Logic.Library.Exceptions
{
    public class UserAlreadyHaveLoanException : Exception
    {
        public UserAlreadyHaveLoanException(string message) :base(message)
        {
        }
    }
}
