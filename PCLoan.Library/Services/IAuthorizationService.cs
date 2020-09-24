using PCLoan.Logic.Library.Models;

namespace PCLoan.Logic.Library.Services
{
    public interface IAuthorizationService
    {
        UserModelDTO AuthorizeUser(UserModelDTO model);
    }
}