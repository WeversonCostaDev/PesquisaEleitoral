namespace PesquisaEleitoral_v2.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        ICandidatoRepository CandidatoRepository { get; }
        IEleitorRepository EleitorRepository { get; }
        IPesquisaRepository PesquisaRepository { get; }
        IIntencaoDeVotoRepository IntencaoDeVotoRepository { get; }
        Task CommitAsync();
    }
}
