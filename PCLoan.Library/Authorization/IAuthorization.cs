using PCLoan.Logic.Library.Models;

namespace PCLoan.Logic.Library.Authorization
{
    interface IAuthorization
    {
        UserModel AuthorizeUser(UserModel user);
    }
}