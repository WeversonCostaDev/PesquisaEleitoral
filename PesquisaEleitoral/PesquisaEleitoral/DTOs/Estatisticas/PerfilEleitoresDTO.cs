using PesquisaEleitoral.Enums;

namespace PesquisaEleitoral.DTOs.Estatisticas
{
    public class PerfilEleitoresDTO
    {
        public int CandidatoId { get; set; }
        public string Nome { get; set; } = string.Empty;

        public int TotalVotos { get; set; }
        public decimal PorcentagemVotos { get; set; }
        public Dictionary<FaixaEtaria, decimal> DistribuicaoFaixaEtaria { get; set; } = null!;
        public Dictionary<ClasseSocial, decimal> DistribuicaoRenda { get; set; } = null!;
        public Dictionary<Escolaridade, decimal> DistribuicaoEscolaridade { get; set; } = null!;
        public Dictionary<Sexo, decimal> DistribuicaoSexo { get; set; } = null!;
    }
}
