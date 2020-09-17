using PCLoan.Library.Models;

namespace PCLoan.Library.Authentication
{
    public interface IAuthentication
    {
        UserModel AuthenticateUser(UserModel user);
    }
}