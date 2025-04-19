using System.Linq.Expressions;

namespace Infrastructure.DatabaseAbstractions
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);
        IEnumerable<T> FindAll(Func<T, bool> expression, params Expression<Func<T, object>>[] includes);
        T FindOrThrow(int id, params Expression<Func<T, object>>[] includes);
        T FindBy(Func<T, bool> expression, params Expression<Func<T, object>>[] includes);

        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void AddRange(IEnumerable<T> entities);
    }
}
