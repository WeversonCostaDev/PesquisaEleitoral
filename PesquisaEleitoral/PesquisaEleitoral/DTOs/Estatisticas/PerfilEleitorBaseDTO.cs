using PesquisaEleitoral.Enums;

namespace PesquisaEleitoral.DTOs.Estatisticas
{
    public class PerfilEleitorBaseDTO
    {

        public Sexo Sexo { get; set; }
        public int Idade { get; set; }
        public Escolaridade Escolaridade { get; set; }
        public decimal Renda { get; set; }

    }
}
