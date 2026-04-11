using Microsoft.EntityFrameworkCore;
using PesquisaEleitoral.DTOs.Estatisticas;
using PesquisaEleitoral.Enums;
using PesquisaEleitoral.Models;
using PesquisaEleitoral.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PesquisaEleitoral.Service
{
    public class IntencaoDeVotoService
    {
        private readonly IUnitOfWork _uow;
        public IntencaoDeVotoService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<int> TotalDeVotosAsync()
        {
            return await _uow.IntencaoDeVotoRepository.TotalDeVotosAsync();
        }
        public async Task<IntencaoDeVoto?> GetByIdAsync(int id)
        {
            return await _uow.IntencaoDeVotoRepository.GetByIdAsync(id);
        }
        public async Task<PerfilEleitoresDTO> PerfilEleitores(int candidatoId)
        {
            var candidato = await _uow.CandidatoRepository.GetByIdAsync(candidatoId)
            ?? throw new KeyNotFoundException("Candidato não encontrado.");

            //cria uma lista de objetos anônimos com os dados de eleitores contidos em cada objeto.
            var dadosEleitores = await _uow.IntencaoDeVotoRepository
                .ObterDadosEleitoresAsync(candidatoId);

            //Quantidade de votos daquele candidato.
            var total = dadosEleitores.Count();
            var totalGeral = await _uow.IntencaoDeVotoRepository.TotalDeVotosAsync();

            //Cria um dicionario separando masculino e feminino como chaves, e porcentagem sendo os valores.
            var distribuicaoSexo = dadosEleitores
                .GroupBy(e => e.Sexo)
                .ToDictionary(
                    g => g.Key,
                    g => total == 0 ? 0 : (double)g.Count() * 100 / total
                );

            //Cria um dicionário com nível de escoladoridade do eleitorado desse candidato.
            var distribuicaoEscolaridade = dadosEleitores
                .GroupBy(e => e.Escolaridade)
                .ToDictionary(
                    g => g.Key,
                    g => total == 0 ? 0 : (double)g.Count() * 100 / total
                );

            //Pega a média de idade entre os eleitores.
            var idadeMedia = total == 0 ? 0 : dadosEleitores.Average(e => e.Idade);
            //Média da renda do eleitorado.
            var rendaMedia = total == 0 ? 0 : dadosEleitores.Average(e => e.Renda);

            var porcentagemDeVotos = totalGeral == 0 ? 0
                : (double)total * 100 / totalGeral;

            return new PerfilEleitoresDTO()
            {
                CandidatoId = candidato.CandidatoId,
                Nome = candidato.Nome,
                TotalVotos = total,
                PorcentagemVotos = porcentagemDeVotos,
                RendaMedia = rendaMedia,
                IdadeMedia = idadeMedia,
                DistribuicaoEscolaridade = distribuicaoEscolaridade,
                DistribuicaoSexo = distribuicaoSexo,
            };
        }
        public async Task<IEnumerable<IntencaoDeVoto>> GetPagedAsync(int take)
        {
            return await _uow.IntencaoDeVotoRepository.GetPagedAsync(take);
        }
        public async Task<IEnumerable<EstatisticaVotoResponseDTO>> EstatisticaPorCandidatoAsync(Regiao? regiao = null)
        {
            return await _uow.IntencaoDeVotoRepository.EstatisticaPorCandidatoAsync(regiao);
        }
        public async Task Create(IntencaoDeVoto intencao)
        {
            _uow.IntencaoDeVotoRepository.Create(intencao);
            await _uow.CommitAsync();
        }
        public async Task Delete(IntencaoDeVoto intencao)
        {
            _uow.IntencaoDeVotoRepository.Delete(intencao);
            await _uow.CommitAsync();
        }

    }
}
