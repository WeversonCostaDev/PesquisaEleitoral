using PesquisaEleitoral.DTOs.Estatisticas;
using PesquisaEleitoral.Enums;
using PesquisaEleitoral.Models;
using PesquisaEleitoral.Pagination.Interfaces;

namespace PesquisaEleitoral.Repositories.Interfaces
{
    public interface IIntencaoDeVotoRepository
    {
        Task<IntencaoDeVoto?> GetByIdAsync(int id);
        Task<int> GetTotalDeVotosAsync();
        Task<EstatisticasEleitorDTO> GetEstatisticaAsync(int candidatoId);
        Task<IEnumerable<SexoDTO>> GetDistribuicaoSexoAsync(int candidatoId);
        Task<IEnumerable<EscolaridadeDTO>> GetDistribuicaoEscolaridadeAsync(int candidatoId);
        Task<bool> JaVotouAsync(int eleitorId);
        Task<IPagedList<IntencaoDeVoto>> GetPagedAsync(IQueryStringPagination parameters);
        Task<IEnumerable<EstatisticaVotoResponseDTO>> EstatisticaPorCandidatoAsync(Regiao? regiao = null);
        IntencaoDeVoto Create(IntencaoDeVoto intencao);
        void Delete(IntencaoDeVoto intencao);
    }    
}
