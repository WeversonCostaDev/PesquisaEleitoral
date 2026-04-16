using PesquisaEleitoral.DTOs.IntencaoDeVotos;
using PesquisaEleitoral.Models;


namespace PesquisaEleitoral.DTOs.Mapping
{
    public static class IntencaoDeVotoDTOMappingExtensions
    {
        public static IntencaoDeVoto ToIntencaoDeVoto(this IntencaoDeVotoDTO intencaoDeVotoDto)
        {
            return new IntencaoDeVoto 
            {
                CandidatoId = intencaoDeVotoDto.CandidatoId,
                EleitorId = intencaoDeVotoDto.EleitorId,
                DataRegistro = intencaoDeVotoDto.DataRegistro,
            };
        }
        public static void UpdateFromDTO(this IntencaoDeVoto intencao, IntencaoDeVotoPutDTO intencaoDeVotoPutDto)
        {
            intencao.CandidatoId = intencaoDeVotoPutDto.CandidatoId;
            intencao.EleitorId = intencaoDeVotoPutDto.EleitorId;
            intencao.DataRegistro = intencaoDeVotoPutDto.DataRegistro;
        }

        public static IntencaoDeVotoResponseDTO ToIntencaoDeVotoResponseDTO(this IntencaoDeVoto intencaoDeVoto)
        {
            return new IntencaoDeVotoResponseDTO
            {
                IntencaoDeVotoId = intencaoDeVoto.IntencaoDeVotoId,
                Candidato = intencaoDeVoto.Candidato.ToCandidatoResponseDTO(),
                Eleitor = intencaoDeVoto.Eleitor.ToEleitorResponseDTO(),
                DataRegistro = intencaoDeVoto.DataRegistro
            };
        }
        public static IEnumerable<IntencaoDeVotoResponseDTO> ToIntencaoDeVotoResponseDTOList(this IEnumerable<IntencaoDeVoto> intencoesDeVoto)
        {
            return intencoesDeVoto.Select(i => i.ToIntencaoDeVotoResponseDTO());
        }
    }
}
