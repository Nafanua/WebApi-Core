using System.Linq;

namespace DAL.Repository
{
    public interface IGenericReposytory<T> where T : class
    {
        IQueryable<T> GetAll();
        void Add(T item);
    }
}
