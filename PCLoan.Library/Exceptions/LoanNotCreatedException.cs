using System;

namespace PCLoan.Logic.Library.Exceptions
{
    public class LoanNotCreatedException : Exception
    {
        public LoanNotCreatedException(string message) : base(message)
        {
        }
    }
}
