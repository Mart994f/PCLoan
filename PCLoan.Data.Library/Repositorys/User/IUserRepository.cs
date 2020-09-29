using PCLoan.Data.Library.Models;

namespace PCLoan.Data.Library.Repositorys
{
    public interface IUserRepository : IRepository<UserModelDAO>
    {
        bool Exsist(string name);

        int GetIdByname(string name);

        string GetUsernameById(int id);

    }
}