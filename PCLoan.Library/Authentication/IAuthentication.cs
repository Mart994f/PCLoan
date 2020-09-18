using PCLoan.Logic.Library.Models;

namespace PCLoan.Logic.Library.Authentication
{
    public interface IAuthentication
    {
        UserModel AuthenticateUser(UserModel user);
    }
}