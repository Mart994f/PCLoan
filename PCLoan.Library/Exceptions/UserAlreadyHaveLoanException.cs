using System;

namespace PCLoan.Logic.Library.Exceptions
{
    class UserAlreadyHaveLoanException : Exception
    {
        public UserAlreadyHaveLoanException(string message) :base(message)
        {
        }
    }
}
