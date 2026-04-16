using System.Linq.Expressions;

namespace PesquisaEleitoral.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<T?> GetByIdAsync(int id);
        Task<bool> VerifyAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetPagedAsync(int take);
        T Create(T entity);
        void Delete(T entity);
    }
}
