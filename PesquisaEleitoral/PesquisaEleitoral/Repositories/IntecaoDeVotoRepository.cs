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
        private async Task<int> TotalDeVotosAsync()
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
            var totalGeral = await TotalDeVotosAsync();

            var result = await _context.IntencoesDeVoto
                .Where(iv => iv.CandidatoId == candidatoId)
                .GroupBy(iv => 1)
                .Select(g => new EstatisticasEleitorDTO
                {
                    TotalVotos = g.Count(),
                    PorcentagemVotos = totalGeral == 0 ? 0 
                    : (double) g.Count() * 100 / totalGeral,
                    IdadeMedia = g.Average(x => x.Eleitor.Idade),
                    RendaMedia = g.Average(x => x.Eleitor.Renda),

                }).FirstOrDefaultAsync();
            
            return result ?? new EstatisticasEleitorDTO();
        }
        public async Task<bool> JaVotou(int eleitorId)
        {
            return await _context.IntencoesDeVoto.AnyAsync(iv => iv.EleitorId == eleitorId);
        }
        public async Task<Dictionary<Sexo, double>> GetDistribuicaoSexoAsync(int candidatoId)
        {

            var result = await _context.IntencoesDeVoto
            .Where(iv => iv.CandidatoId == candidatoId)
            .GroupBy(iv => iv.Eleitor.Sexo)
            .Select(g => new
            {
                Sexo = g.Key,
                Total = g.Count()
            }).ToListAsync();

            //soma total de votos daquele candidato.
            var total = result.Sum(obj => obj.Total);

            return result.ToDictionary(
                item => item.Sexo,
                item => total == 0 ? 0 : (double)item.Total * 100 / total);

        }
        public async Task<Dictionary<Escolaridade, double>> GetDistribuicaoEscolaridadeAsync(int candidatoId)
        {
            var result = await _context.IntencoesDeVoto
                .Where(iv => iv.CandidatoId == candidatoId)
                .GroupBy(iv => iv.Eleitor.Escolaridade)
                .Select(g => new
                {
                    Escolaridade = g.Key,
                    Total = g.Count(),
                })
                .ToListAsync();

            var total = result.Sum(x => x.Total);

            return result.ToDictionary(
                item => item.Escolaridade,
                item => total == 0 ? 0 : (double)item.Total * 100/ total);

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
