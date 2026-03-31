using PesquisaEleitoral.Enums;
using PesquisaEleitoral.Models;

namespace PesquisaEleitoral.Repositories.Interfaces
{
    public interface IIntencaoDeVotoRepository: IRepository<IntencaoDeVoto>
    {
        Task<IEnumerable<IntencaoDeVoto>> GetPorRegiao(Regiao regiao);
    }
}
