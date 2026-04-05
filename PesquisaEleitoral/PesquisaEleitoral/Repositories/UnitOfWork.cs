using PesquisaEleitoral.Data;
using PesquisaEleitoral.Models;
using PesquisaEleitoral.Repositories.Interfaces;

namespace PesquisaEleitoral.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private ICandidatoRepository? _candidatoRepo;
        private IEleitorRepository? _eleitorRepo;
        private IIntencaoDeVotoRepository? _intencaoDeVotoRepo;

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
                return _intencaoDeVotoRepo = _intencaoDeVotoRepo ?? new IntecaoDeVotoRepository(_context);
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
