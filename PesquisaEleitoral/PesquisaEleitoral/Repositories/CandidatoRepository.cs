using PesquisaEleitoral.Data;
using PesquisaEleitoral.Models;
using PesquisaEleitoral.Repositories.Interfaces;

namespace PesquisaEleitoral.Repositories
{
    public class CandidatoRepository : Repository<Candidato>, ICandidatoRepository
    {
        public CandidatoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
