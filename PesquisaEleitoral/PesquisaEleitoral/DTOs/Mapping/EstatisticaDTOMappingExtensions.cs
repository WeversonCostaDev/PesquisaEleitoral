using PesquisaEleitoral.DTOs.Estatisticas;

namespace PesquisaEleitoral.DTOs.Mapping
{
    public static class EstatisticaDTOMappingExtensions
    {
        public static EstatisticasEleitorDTO Vazio(this EstatisticasEleitorDTO entity)
        {
            return new EstatisticasEleitorDTO
            {
                ContagemVotos = 0,
                IdadeMedia = 0,
                RendaMedia = 0,
            };
        }
    }
}
