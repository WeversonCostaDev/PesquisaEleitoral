using PesquisaEleitoral_v2.Data;
using PesquisaEleitoral_v2.Models;
using PesquisaEleitoral_v2.Repositories.Interfaces;

namespace PesquisaEleitoral_v2.Repositories
{
    public class EleitorRepository : Repository<Eleitor>, IEleitorRepository
    {
        public EleitorRepository(AppDbContext context) : base(context)
        {
        }
    }
}
