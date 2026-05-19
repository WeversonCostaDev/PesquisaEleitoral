using Microsoft.EntityFrameworkCore;
using PesquisaEleitoral_v2.Data;
using PesquisaEleitoral_v2.Models;
using PesquisaEleitoral_v2.Repositories.Interfaces;

namespace PesquisaEleitoral_v2.Repositories
{
    public class PesquisaRepository : Repository<Pesquisa>, IPesquisaRepository
    {
        private readonly AppDbContext _context;
        public PesquisaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<Pesquisa?> GetByIdAsync(int pesquisaId)
        {
            return await _context.Pesquisas
                .Where(p => p.PesquisaId == pesquisaId)
                .Include(p => p.Candidatos)
                .FirstOrDefaultAsync();
        }

    }
}
