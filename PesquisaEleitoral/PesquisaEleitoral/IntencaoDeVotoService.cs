using PesquisaEleitoral.DTOs.IntencaoDeVotos;
using PesquisaEleitoral.Enums;
using PesquisaEleitoral.Repositories.Interfaces;

namespace PesquisaEleitoral
{
    public class IntencaoDeVotoService
    {
        private readonly IUnitOfWork _uow;
        
        public IntencaoDeVotoService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /*public Task<IEnumerable<EstatisticaVotoResponseDTO>> Estatistica(Regiao? regiao = null)
        {
            var querry;
            if(regiao is not null)
            {
                querry = _uow.IntencaoDeVotoRepository.Query().Where(i => i.Eleitor.Regiao == regiao.Value);
            }
            var totalGeral = querry.CountAsync();
            
        }*/
    }
}
