using PCLoan.Logic.Library.Models;

namespace PCLoan.Logic.Library.Controllers
{
    public interface ILoginController
    {
        UserModelDTO LoginUser(UserModelDTO model);
    }
}