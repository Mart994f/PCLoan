using System;

namespace PCLoan.Logic.Library.Exceptions
{
    public class CanNotDeleteComputerException : Exception
    {
        public CanNotDeleteComputerException(string message) : base(message)
        {
        }
    }
}
