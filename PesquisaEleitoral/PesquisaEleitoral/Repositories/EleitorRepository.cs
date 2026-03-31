using PesquisaEleitoral.Data;
using PesquisaEleitoral.Models;
using PesquisaEleitoral.Repositories.Interfaces;

namespace PesquisaEleitoral.Repositories
{
    public class EleitorRepository : Repository<Eleitor>, IEleitorRepository
    {
        public EleitorRepository(AppDbContext context) : base(context)
        {
        }
    }
}
