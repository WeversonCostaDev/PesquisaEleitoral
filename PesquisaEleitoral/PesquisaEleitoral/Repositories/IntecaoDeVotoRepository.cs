using Microsoft.EntityFrameworkCore;
using PesquisaEleitoral.Data;
using PesquisaEleitoral.Enums;
using PesquisaEleitoral.Models;
using PesquisaEleitoral.Repositories.Interfaces;

namespace PesquisaEleitoral.Repositories
{
    public class IntecaoDeVotoRepository : Repository<IntencaoDeVoto>, IIntencaoDeVotoRepository
    {
        public IntecaoDeVotoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
