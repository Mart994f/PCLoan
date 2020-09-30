namespace PCLoan.Logic.Library.Services
{
    public interface ILoggingService
    {
        void Log(int userId, string description, int? computerId = null);
    }
}