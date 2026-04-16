namespace PesquisaEleitoral.DTOs.Estatisticas
{
    public class EstatisticasEleitorDTO
    {
        public int TotalVotos { get; set; } = 0;
        public double PorcentagemVotos { get; set; } = 0;
        public double IdadeMedia { get; set; } = 0;
        public decimal RendaMedia { get; set; } = 0;
    }
}
