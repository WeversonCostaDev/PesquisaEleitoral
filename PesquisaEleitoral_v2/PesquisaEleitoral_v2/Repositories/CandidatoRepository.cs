using PesquisaEleitoral_v2.Data;
using PesquisaEleitoral_v2.Models;
using PesquisaEleitoral_v2.Repositories.Interfaces;

namespace PesquisaEleitoral_v2.Repositories
{
    public class CandidatoRepository : Repository<Candidato>, ICandidatoRepository
    {
        public CandidatoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
