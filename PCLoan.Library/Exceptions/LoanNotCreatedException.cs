using System;

namespace PCLoan.Logic.Library.Exceptions
{
    class LoanNotCreatedException : Exception
    {
        public LoanNotCreatedException(string message) : base(message)
        {
        }
    }
}
