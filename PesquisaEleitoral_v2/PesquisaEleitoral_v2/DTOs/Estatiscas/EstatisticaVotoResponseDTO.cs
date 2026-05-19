namespace PesquisaEleitoral_v2.DTOs.Estatiscas
{
    public class EstatisticaVotoResponseDTO
    {   
        public int PesquisaId { get; set; }
        public int CandidatoId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int TotalVotos { get; set; }
        public double Porcentagem { get; set; }

    }
}
