using PesquisaEleitoral.Models;

namespace PesquisaEleitoral.DTOs.Mapping
{
    public static class CandidatoDTOMappingExtensions
    {

        public static Candidato ToCandidato(this CandidatoDTO candidatoDto)
        {
            return new Candidato
            {
                Nome = candidatoDto.Nome,
                Partido = candidatoDto.Partido,
                Numero = candidatoDto.Numero,
            };
        }

        public static CandidatoResponseDTO ToCandidatoResponseDTO(this Candidato candidato)
        {
            return new CandidatoResponseDTO
            {   
                CandidatoId = candidato.CandidatoId,
                Nome = candidato.Nome,
                Numero = candidato.Numero,
                Partido = candidato.Partido,
            };
        }

        public static IEnumerable<CandidatoResponseDTO> ToCandidatosResponseDTOList(this IEnumerable<Candidato> candidatos)
        {
            var candidatosResponseDto = candidatos.Select(c => new CandidatoResponseDTO
            {
                CandidatoId = c.CandidatoId,
                Nome = c.Nome,
                Numero= c.Numero,
                Partido= c.Partido,
            });
            return candidatosResponseDto;
        }

    }
}
