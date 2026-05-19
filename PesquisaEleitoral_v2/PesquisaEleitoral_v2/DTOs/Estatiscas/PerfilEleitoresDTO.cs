using PesquisaEleitoral_v2.Enums;

namespace PesquisaEleitoral_v2.DTOs.Estatiscas
{
    public class PerfilEleitoresDTO
    {   
        public int PesquisaId { get; set; }
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
