using PesquisaEleitoral_v2.DTOs.Eleitores;
using PesquisaEleitoral_v2.Models;

namespace PesquisaEleitoral_v2.DTOs.Mapping
{
    public static class EleitorMappingExtensions
    {
        public static Eleitor ToEleitor(this EleitorDTO eleitorDto)
        {
            return new Eleitor
            {
                Nome = eleitorDto.Nome,
                Renda = eleitorDto.Renda,
                Idade = eleitorDto.Idade,
                Sexo = eleitorDto.Sexo,
                Regiao = eleitorDto.Regiao,
                Escolaridade = eleitorDto.Escolaridade,
                
            };
        }
        public static EleitorResponseDTO ToEleitorResponseDTO(this Eleitor eleitor)
        {
            return new EleitorResponseDTO
            {
                EleitorId = eleitor.EleitorId,
                Nome = eleitor.Nome,
                Renda = eleitor.Renda,
                Idade = eleitor.Idade,
                Sexo = eleitor.Sexo,
                Regiao = eleitor.Regiao,
                Escolaridade = eleitor.Escolaridade,
            };
        }
        public static void UpdateFromDTO(this Eleitor eleitor, EleitorUpdateDTO eleitorPutDto)
        {
            eleitor.Nome = eleitorPutDto.Nome;
            eleitor.Idade = eleitorPutDto.Idade;
            eleitor.Sexo = eleitorPutDto.Sexo;
            eleitor.Regiao = eleitorPutDto.Regiao;
            eleitor.Escolaridade = eleitorPutDto.Escolaridade;
            eleitor.Renda = eleitorPutDto.Renda;

        }
        public static IEnumerable<EleitorResponseDTO> ToEleitoresResponseDTOList(this IEnumerable<Eleitor> eleitores)
        {
            return eleitores.Select(e => e.ToEleitorResponseDTO());
        }
    }
}
