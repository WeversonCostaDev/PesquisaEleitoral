using PesquisaEleitoral.DTOs.Estatisticas;
using PesquisaEleitoral.Enums;
using PesquisaEleitoral.Models;
using System.Linq.Expressions;

namespace PesquisaEleitoral.Repositories.Interfaces
{
    public interface IIntencaoDeVotoRepository
    {
        Task<int> TotalDeVotosAsync();
        Task<IntencaoDeVoto?> GetByIdAsync(int id);
        Task<IEnumerable<IntencaoDeVoto>> GetPagedAsync(int take);
        Task<IEnumerable<PerfilEleitorBaseDTO>> ObterDadosEleitoresAsync(int candidatoId);
        Task<IEnumerable<EstatisticaVotoResponseDTO>> EstatisticaPorCandidatoAsync(Regiao? regiao = null);
        void Create(IntencaoDeVoto intencao);
        void Delete(IntencaoDeVoto intencao);
    }    
}
