using PCLoan.Data.Library.Models;

namespace PCLoan.Data.Library
{
    public interface IUserRepository :IRepository<UserModelDAO>
    {
        bool Exsist(string name);

        int GetIdByname(string name);

    }
}