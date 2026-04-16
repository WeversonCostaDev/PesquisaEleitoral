using Microsoft.EntityFrameworkCore;
using PesquisaEleitoral.Data;
using System.Linq.Expressions;

namespace PesquisaEleitoral.Repositories
{
    public class Repository<T> where T : class
    {   
        private AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context
                .Set<T>()
                .FindAsync(id);
        }
        public async Task<bool> VerifyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }
        public async Task<IEnumerable<T>> GetPagedAsync(int take)
        {
            return await _context
                .Set<T>()
                .AsNoTracking()
                .Take(take)
                .ToListAsync();
        }
        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        
    }
}
