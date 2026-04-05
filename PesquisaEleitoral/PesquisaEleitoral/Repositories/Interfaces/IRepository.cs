using System.Linq.Expressions;

namespace PesquisaEleitoral.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetPagedAsync(int take);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> Query();
        T Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
