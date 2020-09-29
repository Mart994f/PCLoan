using PCLoan.Logic.Library.Models;

namespace PCLoan.Logic.Library.Services
{
    public interface IAuthenticationService
    {
        UserModelDTO AuthenticateUser(UserModelDTO model);
    }
}