using PesquisaEleitoral.Enums;

namespace PesquisaEleitoral.DTOs.Estatisticas
{
    public class PerfilEleitoresDTO
    {
        public int CandidatoId { get; set; }
        public string Nome { get; set; } = string.Empty;

        public int TotalVotos { get; set; }
        public double PorcentagemVotos { get; set; }
        public decimal RendaMedia { get; set; }
        public double IdadeMedia { get; set; }

        public Dictionary<Escolaridade, double> DistribuicaoEscolaridade { get; set; } = null!;
        public Dictionary<Sexo, double> DistribuicaoSexo { get; set; } = null!;
    }
}
