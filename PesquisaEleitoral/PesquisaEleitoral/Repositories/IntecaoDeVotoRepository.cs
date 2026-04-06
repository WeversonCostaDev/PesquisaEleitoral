using Microsoft.EntityFrameworkCore;
using PesquisaEleitoral.Data;
using PesquisaEleitoral.DTOs.Estatisticas;
using PesquisaEleitoral.DTOs.IntencaoDeVotos;
using PesquisaEleitoral.Enums;
using PesquisaEleitoral.Models;
using PesquisaEleitoral.Repositories.Interfaces;

namespace PesquisaEleitoral.Repositories
{
    public class IntecaoDeVotoRepository : Repository<IntencaoDeVoto>, IIntencaoDeVotoRepository
    {
        private readonly AppDbContext _context;
        public IntecaoDeVotoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EstatisticaVotoResponseDTO>> EstatisticaDeVotoPorCandidatoAsync(Regiao? regiao = null)
        {
            IQueryable<IntencaoDeVoto> query = _context.IntencoesDeVoto;

            if (regiao is not null)
            {
                query = query.Where(iv => iv.Eleitor.Regiao == regiao);
            }
            //Conta de forma assícrona no banco de dados, todos os votos gerais ou da regiao caso especificada.
            var TotalGeral = await query.CountAsync();

            var listaDeVotos = await query
                .GroupBy(g => new { g.CandidatoId, g.Candidato.Nome })
                .Select(g => new EstatisticaVotoResponseDTO
                {
                    CandidatoId = g.Key.CandidatoId,
                    Nome = g.Key.Nome,
                    TotalVotos = g.Count(),
                    Porcentagem = TotalGeral == 0 ? 0 : Math.Round((double)g.Count() * 100 / TotalGeral, 2),
                })
                .OrderByDescending(g => g.TotalVotos)
                .ToListAsync();

            return listaDeVotos;
        }
        
        public async Task<IntencaoDeVoto?> GetByIdFullAsync(int id)
        {
            var intencaoDeVoto = await _context.IntencoesDeVoto
                .Include(i => i.Eleitor)
                .Include(i => i.Candidato)
                .FirstOrDefaultAsync(i=> i.IntencaoDeVotoId == id);
            return intencaoDeVoto;
        }
    }
}
