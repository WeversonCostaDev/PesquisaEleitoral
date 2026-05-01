using PesquisaEleitoral.DTOs.Estatisticas;
using PesquisaEleitoral.Enums;
using PesquisaEleitoral.Models;

namespace PesquisaEleitoral.Repositories.Interfaces
{
    public interface IIntencaoDeVotoRepository
    {
        Task<IntencaoDeVoto?> GetByIdAsync(int id);
        Task<int> GetTotalDeVotosAsync();
        Task<EstatisticasEleitorDTO> GetEstatisticaAsync(int candidatoId);
        Task<List<SexoDTO>> GetDistribuicaoSexoAsync(int candidatoId);
        Task<List<EscolaridadeDTO>> GetDistribuicaoEscolaridadeAsync(int candidatoId);
        Task<bool> JaVotou(int eleitorId);
        Task<IEnumerable<IntencaoDeVoto>> GetPagedAsync(int take);
        Task<IEnumerable<EstatisticaVotoResponseDTO>> EstatisticaPorCandidatoAsync(Regiao? regiao = null);
        IntencaoDeVoto Create(IntencaoDeVoto intencao);
        void Delete(IntencaoDeVoto intencao);
    }    
}
