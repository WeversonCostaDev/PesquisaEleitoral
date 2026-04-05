using PesquisaEleitoral.DTOs.Candidatos;
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

        public static Candidato ToCandidato(this CandidatoPutDTO candidatoPutDto)
        {
            return new Candidato
            {   CandidatoId = candidatoPutDto.CandidatoId,
                Nome = candidatoPutDto.Nome,
                Partido = candidatoPutDto.Partido,
                Numero = candidatoPutDto.Numero,
            };
        }

        public static IEnumerable<CandidatoResponseDTO> ToCandidatosResponseDTOList(this IEnumerable<Candidato> candidatos)
        {
            return candidatos.Select(c => c.ToCandidatoResponseDTO());
        }

    }
}
