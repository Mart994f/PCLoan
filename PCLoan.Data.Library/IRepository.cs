using System.Collections.Generic;

namespace PCLoan.Data.Library
{
    public interface IRepository<T> where T : class
    {
        bool Delete(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
        long Insert(T entity);
        bool Update(T entity);
    }
}