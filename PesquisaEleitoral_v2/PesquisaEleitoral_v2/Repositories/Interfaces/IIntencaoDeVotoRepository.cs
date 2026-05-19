using PesquisaEleitoral_v2.DTOs.Estatiscas;
using PesquisaEleitoral_v2.Enums;
using PesquisaEleitoral_v2.Models;
using PesquisaEleitoral_v2.Pagination;

namespace PesquisaEleitoral_v2.Repositories.Interfaces
{
    public interface IIntencaoDeVotoRepository : IRepository<IntencaoDeVoto>
    {
        Task<IPagedList<IntencaoDeVoto>> GetPagedAsync(
            IQueryStringPagination query, int pesquisaId);
        Task<IEnumerable<EstatisticaVotoResponseDTO>> GetEstatisticaPorCandidatoAsync(
            int pesquisaId, Regiao? regiao = null);
        Task<EstatisticasEleitorDTO> GetEstatisticaAsync(
            int pesquisaId, int candidatoId);
        Task<IEnumerable<EscolaridadeDTO>> GetDistribuicaoEscolaridadeAsync(
            int pesquisaId, int candidatoId);
        Task<IEnumerable<SexoDTO>> GetDistribuicaoSexoAsync(
            int pesquisaId, int candidatoId);
        Task<IntencaoDeVoto?> GetByIdAsync(int pesquisaId, int eleitorId);
        Task<IntencaoDeVoto?> GetByEleitorIdPesquisaId(int pesquisaId, int eleitorId);
        Task<int> GetTotalDeVotosPesquisaAsync(int pesquisaId);
        Task<bool> JaVotouAsync(int eleitorId, int pesquisaId);

    }
}
