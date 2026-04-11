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
        public async Task<int> TotalDeVotosAsync()
        {
            return await _context.IntencoesDeVoto.CountAsync();
        }
        public async Task<IntencaoDeVoto?> GetByIdAsync(int id)
        {
            var intencaoDeVoto = await _context.IntencoesDeVoto
                .Include(i => i.Eleitor)
                .Include(i => i.Candidato)
                .FirstOrDefaultAsync(i=> i.IntencaoDeVotoId == id);
            return intencaoDeVoto;
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
        public async Task<IEnumerable<PerfilEleitorBaseDTO>> ObterDadosEleitoresAsync(int candidatoId)
        {

            //cria uma lista de objetos com os dados de eleitores contidos em cada objeto.
            var dadosEleitores = await _context.IntencoesDeVoto
                .Where(iv => iv.CandidatoId == candidatoId)
                .Select(iv => new PerfilEleitorBaseDTO
                {
                    Idade = iv.Eleitor.Idade,
                    Sexo = iv.Eleitor.Sexo,
                    Escolaridade = iv.Eleitor.Escolaridade,
                    Renda = iv.Eleitor.Renda,
                })
                .ToListAsync();
            return dadosEleitores;
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
        public void Create(IntencaoDeVoto intencao)
        {
            _context.Add(intencao);
        }
        public void Delete(IntencaoDeVoto intencao)
        {
            _context.Remove(intencao);
        }
    }
}
