using Microsoft.EntityFrameworkCore;
using PesquisaEleitoral.Data;
using PesquisaEleitoral.DTOs.Estatisticas;
using PesquisaEleitoral.Enums;
using PesquisaEleitoral.Models;
using PesquisaEleitoral.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PesquisaEleitoral.Repositories
{
    public class IntecaoDeVotoRepository : IIntencaoDeVotoRepository
    {
        private readonly AppDbContext _context;
        public IntecaoDeVotoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> GetTotalDeVotosAsync()
        {
            return await _context.IntencoesDeVoto.CountAsync();
        }
        public async Task<IntencaoDeVoto?> GetByIdAsync(int id)
        {
            var intencaoDeVoto = await _context.IntencoesDeVoto
                .Include(i => i.Eleitor)
                .Include(i => i.Candidato)
                .FirstOrDefaultAsync(i => i.IntencaoDeVotoId == id);
            return intencaoDeVoto;
        }
        public async Task<EstatisticasEleitorDTO> GetEstatisticaAsync(int candidatoId)
        {
            var result = await _context.IntencoesDeVoto
                .Where(iv => iv.CandidatoId == candidatoId)
                .GroupBy(iv => 1)
                .Select(g => new EstatisticasEleitorDTO
                {
                    ContagemVotos = g.Count(),
                    IdadeMedia = g.Average(x => x.Eleitor.Idade),
                    RendaMedia = g.Average(x => x.Eleitor.Renda),
                }).FirstOrDefaultAsync();
            
            return result ?? new EstatisticasEleitorDTO();
        }
        public async Task<bool> JaVotou(int eleitorId)
        {
            return await _context.IntencoesDeVoto.AnyAsync(iv => iv.EleitorId == eleitorId);
        }
        public async Task<List<SexoDTO>> GetDistribuicaoSexoAsync(int candidatoId)
        {
            var result = await _context.IntencoesDeVoto
            .Where(iv => iv.CandidatoId == candidatoId)
            .GroupBy(iv => iv.Eleitor.Sexo)
            .Select(g => new SexoDTO
            {
                Sexo = g.Key,
                Total = g.Count()
            }).ToListAsync();

            return result;
        }
        public async Task<List<EscolaridadeDTO>> GetDistribuicaoEscolaridadeAsync(int candidatoId)
        {
            var result = await _context.IntencoesDeVoto
                .Where(iv => iv.CandidatoId == candidatoId)
                .GroupBy(iv => iv.Eleitor.Escolaridade)
                .Select(g => new EscolaridadeDTO
                {
                    Escolaridade = g.Key,
                    Total = g.Count(),
                })
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<IntencaoDeVoto>> GetPagedAsync(int take)
        {
            var intencoesDeVoto = await _context.IntencoesDeVoto
                .AsNoTracking()
                .Include(i => i.Eleitor)
                .Include(i => i.Candidato)
                .Take(100)
                .ToListAsync();

            return intencoesDeVoto;
        }
        public async Task<IEnumerable<EstatisticaVotoResponseDTO>> EstatisticaPorCandidatoAsync(Regiao? regiao = null)
        {
            IQueryable<IntencaoDeVoto> query = _context.IntencoesDeVoto;

            if (regiao is not null)
                query = query.Where(iv => iv.Eleitor.Regiao == regiao);

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
        public IntencaoDeVoto Create(IntencaoDeVoto intencao)
        {
            _context.Add(intencao);
            return intencao;
        }
        public void Delete(IntencaoDeVoto intencao)
        {
            _context.Remove(intencao);
        }        
    }
}
