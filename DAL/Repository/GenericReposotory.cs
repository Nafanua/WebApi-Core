using System.Linq;

namespace DAL.Repository
{
    public class GenericReposotory<T> : IGenericReposytory<T> where T : class
    {
        private ModelContext context;

        public GenericReposotory(ModelContext context)
        {
            this.context = context;
        }

        public void Add(T item)
        {
            context.Set<T>().Add(item);
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = context.Set<T>();

            return query;
        }
    }
}
