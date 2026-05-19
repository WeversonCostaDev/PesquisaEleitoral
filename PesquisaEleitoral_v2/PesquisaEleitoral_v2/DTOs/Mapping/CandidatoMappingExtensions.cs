using PesquisaEleitoral_v2.DTOs.Candidatos;
using PesquisaEleitoral_v2.Models;

namespace PesquisaEleitoral_v2.DTOs.Mapping
{
    public static class CandidatoMappingExtensions
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

        public static void UpdateFromDTO(this Candidato candidato, CandidatoUpdateDTO candidatoPutDto)
        {
            candidato.CandidatoId = candidatoPutDto.CandidatoId;
            candidato.Nome = candidatoPutDto.Nome;
            candidato.Partido = candidatoPutDto.Partido;
            candidato.Numero = candidatoPutDto.Numero;
        }

        public static IEnumerable<CandidatoResponseDTO> ToCandidatosResponseDTOList(
            this IEnumerable<Candidato> candidatos)
        {
            return candidatos.Select(c => c.ToCandidatoResponseDTO());
        }
    }
}
