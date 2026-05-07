using PesquisaEleitoral.Pagination;
using PesquisaEleitoral.Pagination.Interfaces;
using System.Linq.Expressions;

namespace PesquisaEleitoral.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<bool> VerifyAsync(Expression<Func<T, bool>> predicate);
        Task<IPagedList<T>>GetPagedAsync(IQueryStringPagination query);
        T Create(T entity);
        void Delete(T entity);
    }
}
