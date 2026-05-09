namespace PesquisaEleitoral.DTOs.Estatisticas
{
    public class EstatisticasEleitorDTO
    {
        public int ContagemVotos { get; set; } = 0;
        public IEnumerable<int> FaixasEtarias { get; set; } = Enumerable.Empty<int>();
        public IEnumerable<decimal> Rendas { get; set; } = Enumerable.Empty<decimal>();
    }
}
