using System.Linq.Expressions;

namespace PesquisaEleitoral.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<T?> GetById(int id);
        Task<IEnumerable<T>> GetAll(int take);
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate);
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
