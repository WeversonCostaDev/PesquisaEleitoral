using PesquisaEleitoral_v2.DTOs.Estatiscas;
using PesquisaEleitoral_v2.DTOs.IntencoesDeVoto;
using PesquisaEleitoral_v2.Enums;

namespace PesquisaEleitoral_v2.Services
{
    public interface IPesquisaService
    {
        Task<IEnumerable<EstatisticaVotoResponseDTO>> GetEstatisticaPorCandidatoAsync(
            int pesquisaId, Regiao? regiao = null);
        Task<PerfilEleitoresDTO> GetPerfilEleitores(int pesquisaId, int candidatoId);
        Task<IntencaoDeVotoResponseDTO> GetVotoByIdAsync(int pesquisaId, int eleitorId);
        Task<IntencaoDeVotoResponseDTO> VotarAsync(IntencaoDeVotoDTO votoDto);
        Task AtualizaVotoEleitorAsync(IntencaoDeVotoUpdateDTO votoUpdate);
        Task DeleteVotoAsync(int pesquisaId, int eleitorId);
    }
}
