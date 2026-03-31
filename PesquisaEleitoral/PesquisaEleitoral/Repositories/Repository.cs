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

        public async Task<T?> GetById(int id)
        {
            return await _context
                .Set<T>()
                .FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll(int take)
        {
            return await _context
                .Set<T>()
                .AsNoTracking()
                .Take(take)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate)
        {
            return await _context
                .Set<T>()
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }
        
        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public T Update(T entity)
        {   
            _context.Set<T>().Update(entity);
            return entity;
        }

        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return entity;
        }
    }
}
