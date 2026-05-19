using Microsoft.EntityFrameworkCore;
using PesquisaEleitoral_v2.Data;
using PesquisaEleitoral_v2.DTOs.Estatiscas;
using PesquisaEleitoral_v2.Enums;
using PesquisaEleitoral_v2.Models;
using PesquisaEleitoral_v2.Pagination;
using PesquisaEleitoral_v2.Repositories.Interfaces;

namespace PesquisaEleitoral_v2.Repositories
{
    public class IntencaoDeVotoRepository : Repository<IntencaoDeVoto>, IIntencaoDeVotoRepository
    {
        private readonly AppDbContext _context;
        public IntencaoDeVotoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IPagedList<IntencaoDeVoto>> GetPagedAsync(
            IQueryStringPagination query, int pesquisaId)
        {
            var count = await GetTotalDeVotosPesquisaAsync(pesquisaId);

            var items = await _context.IntencoesDeVoto
                .Where(iv => iv.PesquisaId == pesquisaId)
                .AsNoTracking()
                .Include(iv => iv.Pesquisa)
                    .ThenInclude(p => p.Candidatos)
                .Include(iv => iv.Eleitor)
                .Include(iv => iv.Candidato)
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return new PagedList<IntencaoDeVoto>(items, count, query.PageSize, query.PageNumber);
        }
        public async Task<IEnumerable<EstatisticaVotoResponseDTO>> GetEstatisticaPorCandidatoAsync(
            int pesquisaId, Regiao? regiao = null)
        {
            IQueryable<IntencaoDeVoto> query = _context.IntencoesDeVoto
                .Where(iv => iv.PesquisaId == pesquisaId);

            if (regiao is not null)
                query = query.Where(iv => iv.Eleitor.Regiao == regiao);

            //Conta de forma assícrona no banco de dados, todos os votos gerais ou da regiao caso especificada.
            var TotalGeral = await query.CountAsync();

            var listaDeVotos = await query
                .GroupBy(g => new { g.CandidatoId, g.Candidato.Nome, g.PesquisaId})
                .Select(g => new EstatisticaVotoResponseDTO
                {   
                    PesquisaId = g.Key.PesquisaId,
                    CandidatoId = g.Key.CandidatoId,
                    Nome = g.Key.Nome,
                    TotalVotos = g.Count(),
                    Porcentagem = TotalGeral == 0 ? 0 : Math.Round((double)g.Count() * 100 / TotalGeral, 2),
                })
                .OrderByDescending(g => g.TotalVotos)
                .ToListAsync();
            return listaDeVotos;
        }
        public async Task<EstatisticasEleitorDTO> GetEstatisticaAsync(
            int pesquisaId,int candidatoId)
        {
            var result = await _context.IntencoesDeVoto
                .Where(iv => iv.PesquisaId == pesquisaId)
                .Where(iv => iv.CandidatoId == candidatoId)
                .GroupBy(iv => 1)
                .Select(g => new EstatisticasEleitorDTO
                {
                    ContagemVotos = g.Count(),
                    FaixasEtarias = g.Select(g => g.Eleitor.Idade),
                    Rendas = g.Select(g => g.Eleitor.Renda),
                }).FirstOrDefaultAsync();

            return result ?? new EstatisticasEleitorDTO();
        }
        public async Task<IEnumerable<EscolaridadeDTO>> GetDistribuicaoEscolaridadeAsync(
            int pesquisaId,int candidatoId)
        {
            var result = await _context.IntencoesDeVoto
                .Where(iv => iv.PesquisaId == pesquisaId)
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
        public async Task<IEnumerable<SexoDTO>> GetDistribuicaoSexoAsync(
            int pesquisaId, int candidatoId)
        {
            var result = await _context.IntencoesDeVoto
            .Where(iv => iv.PesquisaId == pesquisaId)
            .Where(iv => iv.CandidatoId == candidatoId)
            .GroupBy(iv => iv.Eleitor.Sexo)
            .Select(g => new SexoDTO
            {
                Sexo = g.Key,
                Total = g.Count()
            }).ToListAsync();
            return result;
        }
        public async Task<IntencaoDeVoto?> GetByIdAsync(int pesquisaId, int eleitorId)
        {
            var intencaoDeVoto = await _context.IntencoesDeVoto
                .Where(iv => iv.PesquisaId == pesquisaId)
                .Include(i => i.Pesquisa)
                    .ThenInclude(p => p.Candidatos)
                .Include(i => i.Eleitor)
                .Include(i => i.Candidato)
                .FirstOrDefaultAsync(i => i.EleitorId == eleitorId);
            return intencaoDeVoto;
        }
        public async Task<IntencaoDeVoto?> GetByEleitorIdPesquisaId(int pesquisaId, int eleitorId)
        {
            var intencaoDeVoto = await _context.IntencoesDeVoto
                .Where(iv => iv.PesquisaId == pesquisaId)
                .FirstOrDefaultAsync(iv => iv.EleitorId == eleitorId);
            return intencaoDeVoto;
        }

        public async Task<int> GetTotalDeVotosPesquisaAsync(int pesquisaId)
        {
            return await _context.IntencoesDeVoto
                .Where(iv => iv.PesquisaId == pesquisaId)
                .CountAsync();
        }
        public async Task<bool> JaVotouAsync(int eleitorId, int pesquisaId)
        {
            return await _context.IntencoesDeVoto
                .Where(iv => iv.PesquisaId == pesquisaId)
                .AnyAsync(iv => iv.EleitorId == eleitorId);
        }
        
    }
}
