using PesquisaEleitoral.DTOs.Estatisticas;
using PesquisaEleitoral.Enums;
using PesquisaEleitoral.Models;
using System.Linq.Expressions;

namespace PesquisaEleitoral.Repositories.Interfaces
{
    public interface IIntencaoDeVotoRepository
    {
        Task<IntencaoDeVoto?> GetByIdAsync(int id);
        Task<EstatisticasEleitorDTO> GetEstatisticaAsync(int candidatoId);
        Task<Dictionary<Sexo, double>> GetDistribuicaoSexoAsync(int candidatoId);
        Task<Dictionary<Escolaridade, double>> GetDistribuicaoEscolaridadeAsync(int candidatoId);
        Task<bool> JaVotou(int eleitorId);
        Task<IEnumerable<IntencaoDeVoto>> GetPagedAsync(int take);
        Task<IEnumerable<EstatisticaVotoResponseDTO>> EstatisticaPorCandidatoAsync(Regiao? regiao = null);
        IntencaoDeVoto Create(IntencaoDeVoto intencao);
        void Delete(IntencaoDeVoto intencao);
    }    
}
