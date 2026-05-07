using PesquisaEleitoral.DTOs.Estatisticas;
using PesquisaEleitoral.DTOs.IntencaoDeVotos;
using PesquisaEleitoral.Enums;
using PesquisaEleitoral.Models;
using PesquisaEleitoral.Pagination.Interfaces;

namespace PesquisaEleitoral.Services
{
    public interface IIntencaoDeVotoService
    {
        Task<IntencaoDeVotoResponseDTO> GetByIdAsync(int id);
        Task<PerfilEleitoresDTO> GetPerfilEleitores(int candidatoId);
        Task<IPagedList<IntencaoDeVoto>> GetPagedAsync(IQueryStringPagination parameters);
        Task<IEnumerable<EstatisticaVotoResponseDTO>> EstatisticaPorCandidatoAsync(Regiao? regiao = null);
        Task<IntencaoDeVotoResponseDTO> CreateAsync(IntencaoDeVotoDTO intencao);
        Task UpdateAsync(IntencaoDeVotoPutDTO intencaoDeVotoPutDTO);
        Task DeleteAsync(int id);
    }
}
