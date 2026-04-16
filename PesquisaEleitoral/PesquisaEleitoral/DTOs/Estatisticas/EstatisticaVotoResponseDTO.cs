namespace PesquisaEleitoral.DTOs.Estatisticas
{
    public class EstatisticaVotoResponseDTO
    {
        public int CandidatoId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int TotalVotos { get; set; }
        public double Porcentagem { get; set; }
    }
}
