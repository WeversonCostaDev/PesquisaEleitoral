namespace PesquisaEleitoral.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public ICandidatoRepository CandidatoRepository { get; }
        public IEleitorRepository EleitorRepository { get; }

        public Task CommitAsync();

    }
}
