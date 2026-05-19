using PesquisaEleitoral_v2.DTOs.IntencoesDeVoto;
using PesquisaEleitoral_v2.Models;

namespace PesquisaEleitoral_v2.DTOs.Mapping
{
    public static class IntencaoDeVotoMappingExtensions
    {
        public static IntencaoDeVoto ToIntencaoDeVoto(this IntencaoDeVotoDTO intencaoDeVotoDto)
        {
            return new IntencaoDeVoto
            {
                CandidatoId = intencaoDeVotoDto.CandidatoId,
                EleitorId = intencaoDeVotoDto.EleitorId,
                PesquisaId = intencaoDeVotoDto.PesquisaId,
                DataRegistro = intencaoDeVotoDto.DataRegistro,
            };
        }
        public static IntencaoDeVotoResponseDTO ToIntencaoDeVotoResponseDTO(this IntencaoDeVoto intencaoDeVoto)
        {
            return new IntencaoDeVotoResponseDTO
            {
                IntencaoDeVotoId = intencaoDeVoto.IntencaoDeVotoId,
                Candidato = intencaoDeVoto.Candidato.ToCandidatoResponseDTO(),
                Eleitor = intencaoDeVoto.Eleitor.ToEleitorResponseDTO(),
                Pesquisa = intencaoDeVoto.Pesquisa.ToPesquisaResponseDTO(),
                DataRegistro = intencaoDeVoto.DataRegistro
            };
        }

        public static void UpdateFromDTO(this IntencaoDeVoto intencao, IntencaoDeVotoUpdateDTO intencaoDeVotoPutDto)
        {
            intencao.CandidatoId = intencaoDeVotoPutDto.CandidatoId;
            intencao.DataRegistro = intencaoDeVotoPutDto.DataRegistro;
        }

    }
}
