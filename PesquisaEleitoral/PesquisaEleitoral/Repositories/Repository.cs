using Microsoft.EntityFrameworkCore;
using PesquisaEleitoral.Data;

namespace PesquisaEleitoral.Repositories
{
    public class Repository<T> where T : class
    {   
        private AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        protected IQueryable<T> Get()
        {
            return _context.Set<T>();
        }
        
        protected T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        protected T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }

        protected T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return entity;
        }
    }
}
