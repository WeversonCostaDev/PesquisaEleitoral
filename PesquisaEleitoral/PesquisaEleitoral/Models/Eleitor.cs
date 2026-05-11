using PesquisaEleitoral.Enums;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace PesquisaEleitoral.Models
{
    public class Eleitor
    {
        public int EleitorId { get; set; }
        [Required]
        [StringLength(40)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [Range(16,120)]
        public int Idade { get; set; }

        [Required]
        public Sexo Sexo { get; set; }

        [Required]
        public Regiao Regiao { get; set; }
        
        [Required]
        public Escolaridade Escolaridade { get; set; }

        [Required]
        public decimal Renda { get; set; }

        [JsonIgnore]
        public IntencaoDeVoto? IntencaoDeVoto { get; set; }
        public FaixaEtaria FaixaEtaria
        {
            get
            {
                return Idade switch
                {
                    >= 16 and <= 29 => FaixaEtaria.Jovem,
                    > 30 and 59 => FaixaEtaria.Adulto,
                    _ => FaixaEtaria.Idoso,
                };
            }
        }
        public ClasseSocial ClasseSocial
        {
            get
            {
                return Renda switch
                {
                    >= 0 and <= 2000 => ClasseSocial.Baixa,
                    >2000 and <= 10000 => ClasseSocial.Media,
                    _ => ClasseSocial.Alta
                };
            }
        }
    }
}
