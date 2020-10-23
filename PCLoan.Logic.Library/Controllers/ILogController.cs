using PCLoan.Logic.Library.Models;
using System.Collections.Generic;

namespace PCLoan.Logic.Library.Controllers
{
    public interface ILogController
    {
        List<LogModelDTO> GetLogByComputerId(int computerId);
        List<LogModelDTO> GetLogByUsername(string username);
        List<LogModelDTO> GetLogs();
    }
}