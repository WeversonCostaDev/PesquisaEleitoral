using PesquisaEleitoral.DTOs.Candidatos;
using PesquisaEleitoral.DTOs.Eleitores;
using PesquisaEleitoral.Models;

namespace PesquisaEleitoral.DTOs.Mapping
{
    public static class EleitorDTOMappingExtensions
    {
        public static Eleitor ToEleitor(this EleitorDTO eleitorDto)
        {
            return new Eleitor
            {
                Nome = eleitorDto.Nome,
                Idade = eleitorDto.Idade,
                Sexo = eleitorDto.Sexo,
                Regiao = eleitorDto.Regiao,
                Escolaridade = eleitorDto.Escolaridade,
                Renda = eleitorDto.Renda,
            };
        }
        public static void UpdateFromDTO(this Eleitor eleitor, EleitorPutDTO eleitorPutDto)
        {
            eleitor.Nome = eleitorPutDto.Nome;
            eleitor.Idade = eleitorPutDto.Idade;
            eleitor.Sexo = eleitorPutDto.Sexo;
            eleitor.Regiao = eleitorPutDto.Regiao;
            eleitor.Escolaridade = eleitorPutDto.Escolaridade;
            eleitor.Renda = eleitorPutDto.Renda;

        }
        public static EleitorResponseDTO ToEleitorResponseDTO(this Eleitor eleitor)
        {
            return new EleitorResponseDTO
            {   
                EleitorId = eleitor.EleitorId,
                Nome = eleitor.Nome,
                Idade = eleitor.Idade,
                Sexo = eleitor.Sexo,
                Regiao = eleitor.Regiao,
                Escolaridade = eleitor.Escolaridade,
                Renda = eleitor.Renda,
            };
        }
        public static IEnumerable<EleitorResponseDTO> ToEleitoresResponseDTOList(this IEnumerable<Eleitor> eleitores)
        {
            return eleitores.Select(e => e.ToEleitorResponseDTO());   
        }
    }
}
