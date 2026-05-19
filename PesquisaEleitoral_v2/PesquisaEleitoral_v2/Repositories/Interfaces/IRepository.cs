using PesquisaEleitoral_v2.Pagination;
using System.Linq.Expressions;

namespace PesquisaEleitoral_v2.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<IPagedList<T>> GetPagedAsync(IQueryStringPagination query);
        Task<T?> GetByIdAsync(int id);
        T Create(T entity);
        void Delete(T entity);
    }
}
