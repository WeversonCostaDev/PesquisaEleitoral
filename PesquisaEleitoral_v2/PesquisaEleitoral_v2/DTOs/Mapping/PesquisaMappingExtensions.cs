using PesquisaEleitoral_v2.DTOs.Pesquisas;
using PesquisaEleitoral_v2.Models;
namespace PesquisaEleitoral_v2.DTOs.Mapping
{
    public static class PesquisaMappingExtensions
    {
        public static Pesquisa ToPesquisa(this PesquisaDTO pesquiasDto)
        {
            return new Pesquisa
            {
                Nome = pesquiasDto.Nome,
                Localidade = pesquiasDto.Localidade,
                Descricao = pesquiasDto.Descricao,
                Cargo = pesquiasDto.Cargo,
                DataCriacao = pesquiasDto.DataCriacao
            };
        }
        public static PesquisaResponseDTO ToPesquisaResponseDTO(this Pesquisa pesquisa)
        {
            return new PesquisaResponseDTO
            {
                PesquisaId = pesquisa.PesquisaId,
                Nome = pesquisa.Nome,
                Localidade = pesquisa.Localidade,
                Descricao = pesquisa.Descricao,
                Cargo = pesquisa.Cargo,
                DataCriacao = pesquisa.DataCriacao,
                Candidatos = pesquisa.Candidatos.ToCandidatosResponseDTOList(),
            };
        }

    }
}
