using PesquisaEleitoral.DTOs.Estatisticas;
using PesquisaEleitoral.DTOs.IntencaoDeVotos;
using PesquisaEleitoral.Enums;
using PesquisaEleitoral.Models;

namespace PesquisaEleitoral.Repositories.Interfaces
{
    public interface IIntencaoDeVotoRepository : IRepository<IntencaoDeVoto>
    {
        Task<IEnumerable<EstatisticaVotoResponseDTO>> EstatisticaDeVotoPorCandidatoAsync(Regiao? regiao = null);
        Task<IntencaoDeVoto?> GetByIdFullAsync(int id);
    }    
}
