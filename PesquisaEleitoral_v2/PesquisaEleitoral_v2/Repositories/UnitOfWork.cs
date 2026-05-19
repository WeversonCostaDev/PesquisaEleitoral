using PesquisaEleitoral_v2.Data;
using PesquisaEleitoral_v2.Repositories.Interfaces;

namespace PesquisaEleitoral_v2.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ICandidatoRepository? _candidatoRepo;
        private IEleitorRepository? _eleitorRepo;
        private IIntencaoDeVotoRepository? _intencaoDeVotoRepo;
        private IPesquisaRepository? _pesquisaRepo;

        private AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICandidatoRepository CandidatoRepository
        {
            get
            {
                return _candidatoRepo = _candidatoRepo ?? new CandidatoRepository(_context);
            }
        }

        public IEleitorRepository EleitorRepository
        {
            get
            {
                return _eleitorRepo = _eleitorRepo ?? new EleitorRepository(_context);
            }
        }

        public IIntencaoDeVotoRepository IntencaoDeVotoRepository
        {
            get
            {
                return _intencaoDeVotoRepo = _intencaoDeVotoRepo ?? new IntencaoDeVotoRepository(_context);
            }
        }

        public IPesquisaRepository PesquisaRepository
        {
            get
            {
                return _pesquisaRepo= _pesquisaRepo ?? new PesquisaRepository(_context);
            }
        }
      
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    
}
}
