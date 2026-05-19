using Microsoft.EntityFrameworkCore;
using PesquisaEleitoral_v2.Data;
using PesquisaEleitoral_v2.Pagination;
using PesquisaEleitoral_v2.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PesquisaEleitoral_v2.Repositories
{
    public class Repository<T> : IRepository<T> where T: class
    {
        private AppDbContext _context;
        public Repository(AppDbContext context) 
        {
            _context = context;
        }

        public async virtual Task<IPagedList<T>> GetPagedAsync(IQueryStringPagination query)
        {
            var count = await _context.Set<T>().CountAsync();

            var items = await _context
                .Set<T>()
                .AsNoTracking()
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return new PagedList<T>(items, count, query.PageSize, query.PageNumber);
        }
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _context
                .Set<T>()
                .FindAsync(id);
        }
        public virtual T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }
        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<bool> VerifyAsync(Expression<Func<T,bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }
    }
}
