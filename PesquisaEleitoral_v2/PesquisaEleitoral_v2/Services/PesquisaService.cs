using PesquisaEleitoral_v2.DTOs.Estatiscas;
using PesquisaEleitoral_v2.DTOs.IntencoesDeVoto;
using PesquisaEleitoral_v2.DTOs.Mapping;
using PesquisaEleitoral_v2.Enums;
using PesquisaEleitoral_v2.Repositories.Interfaces;

namespace PesquisaEleitoral_v2.Services
{
    public class PesquisaService : IPesquisaService
    {
        private IUnitOfWork _uow;
        public PesquisaService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IEnumerable<EstatisticaVotoResponseDTO>> GetEstatisticaPorCandidatoAsync(
            int pesquisaId, Regiao? regiao = null)
        {
            return await _uow.IntencaoDeVotoRepository.GetEstatisticaPorCandidatoAsync(pesquisaId, regiao);
        }
        public async Task<PerfilEleitoresDTO> GetPerfilEleitores(int pesquisaId, int candidatoId)
        {
            var pesquisa = await _uow.PesquisaRepository.GetByIdAsync(pesquisaId)
                ?? throw new KeyNotFoundException($"Pesquisa de id {pesquisaId} não encontrada."); ;

            var candidato = pesquisa.Candidatos.FirstOrDefault(c => c.CandidatoId == candidatoId)
                ?? throw new KeyNotFoundException($"Candidato de id {candidatoId} não encontrado.");

            int totalGeral = await _uow.IntencaoDeVotoRepository
                .GetTotalDeVotosPesquisaAsync(pesquisaId);

            var estatisticas = await _uow.IntencaoDeVotoRepository.GetEstatisticaAsync(
                pesquisa.PesquisaId, candidato.CandidatoId);

            var escolaridade = await _uow.IntencaoDeVotoRepository.GetDistribuicaoEscolaridadeAsync(
                pesquisa.PesquisaId, candidato.CandidatoId);

            var sexo = await _uow.IntencaoDeVotoRepository.GetDistribuicaoSexoAsync(
                pesquisa.PesquisaId, candidato.CandidatoId);

            //Calcula a porcentagem de votos
            decimal porcentagemVotos = CalculaPorcentagem(estatisticas.ContagemVotos, totalGeral);

            //Converte a lista de contagem de votos por escolaridade, para um dicionário com a porcentagem como valor. 
            decimal totalEscolaridade = escolaridade.Sum(obj => obj.Total);
            Dictionary<Escolaridade, decimal> dictEscolaridade = escolaridade.ToDictionary(
                item => item.Escolaridade,
                item => CalculaPorcentagem(item.Total, totalEscolaridade));

            //Converte a lista com contagem de votos por sexo, para um dicionário com a porcentagem como valor.
            decimal totalSexo = sexo.Sum(obj => obj.Total);
            Dictionary<Sexo, decimal> dictSexo = sexo.ToDictionary(
                item => item.Sexo,
                item => CalculaPorcentagem(item.Total, totalSexo));

            var dictFaixaEtaria = CalculaDistribuicao<int, FaixaEtaria>
              (estatisticas.FaixasEtarias, IdentificaFaixaEtaria, estatisticas.ContagemVotos);

            var dictClasseSocial = CalculaDistribuicao<decimal, ClasseSocial>
                (estatisticas.Rendas, IdentificaClasseSocial, estatisticas.ContagemVotos);

            var result = new PerfilEleitoresDTO
            {
                PesquisaId = pesquisa.PesquisaId,
                CandidatoId = candidato.CandidatoId,
                Nome = candidato.Nome,
                TotalVotos = estatisticas.ContagemVotos,
                PorcentagemVotos = porcentagemVotos,
                DistribuicaoFaixaEtaria = dictFaixaEtaria,
                DistribuicaoRenda = dictClasseSocial,
                DistribuicaoEscolaridade = dictEscolaridade,
                DistribuicaoSexo = dictSexo
            };
            return result;
        }
        public async Task<IntencaoDeVotoResponseDTO> GetVotoByIdAsync(int pesquisaId, int eleitorId)
        {
            var intencaoDeVoto = await _uow.IntencaoDeVotoRepository.GetByIdAsync(pesquisaId, eleitorId)
                ?? throw new KeyNotFoundException("Intenção de voto não encontrada.");

            var intencoesDeVotoDto = intencaoDeVoto.ToIntencaoDeVotoResponseDTO();
            return intencoesDeVotoDto;
        }
        public async Task<IntencaoDeVotoResponseDTO> VotarAsync(IntencaoDeVotoDTO votoDto)
        {
            var verifyEleitor = await _uow.EleitorRepository
                .GetByIdAsync(votoDto.EleitorId);

            if (verifyEleitor is null)
                throw new KeyNotFoundException($"Não há registro de eleitor de id {votoDto.EleitorId}.");

            var verifyPesquisa = await _uow.PesquisaRepository.GetByIdAsync(votoDto.PesquisaId);
            if (verifyPesquisa is null)
                throw new KeyNotFoundException($"Não há registro de pesquisa de id {votoDto.PesquisaId}.");

            var verifyCandidato = verifyPesquisa.Candidatos.FirstOrDefault(c => c.CandidatoId == votoDto.CandidatoId);

            if (verifyCandidato is null)
                throw new KeyNotFoundException($"Não há registro de candidato de id {votoDto.CandidatoId} " +
                    $"nessa pesquisa");

            var verifyVotoEleitor = await _uow.IntencaoDeVotoRepository
                .JaVotouAsync(votoDto.EleitorId, votoDto.PesquisaId);

            if (verifyVotoEleitor)
                throw new InvalidOperationException("Não foi possível registrar o voto," +
                    " pois este eleitor já possui voto registrado nessa pesquisa.");

            var voto = votoDto.ToIntencaoDeVoto();
            voto = _uow.IntencaoDeVotoRepository.Create(voto);
            await _uow.CommitAsync();

            voto = await _uow.IntencaoDeVotoRepository
                .GetByIdAsync(voto.PesquisaId, voto.EleitorId)
                ?? throw new KeyNotFoundException("Erro ao recuperar a intenção de voto");

            var intencaoResponseDto = voto.ToIntencaoDeVotoResponseDTO();

            return intencaoResponseDto;
        }
        public async Task AtualizaVotoEleitorAsync(IntencaoDeVotoUpdateDTO votoUpdate)
        {
            var pesquisa = await _uow.PesquisaRepository.GetByIdAsync(votoUpdate.PesquisaId)
                ?? throw new InvalidOperationException($"Não há registro da pesquisa " +
                $"de id {votoUpdate.PesquisaId}.");

            var candidato = pesquisa.Candidatos
                .FirstOrDefault(c => c.CandidatoId == votoUpdate.CandidatoId)
                ?? throw new InvalidOperationException($"Não há registro do candidato " +
                $"de id {votoUpdate.CandidatoId}.");

            var eleitor = await _uow.EleitorRepository.GetByIdAsync(votoUpdate.EleitorId)
                ?? throw new InvalidOperationException($"Não há registro do eleitor" +
                $"de id {votoUpdate.EleitorId}.");

            var voto = await _uow.IntencaoDeVotoRepository.GetByEleitorIdPesquisaId(
                pesquisa.PesquisaId, eleitor.EleitorId)
                ?? throw new InvalidOperationException($"Não há registro da intenção " +
                $"de voto do eleitor de id {votoUpdate.EleitorId} "+
                $"na pesquisa de id {votoUpdate.PesquisaId}.");

            voto.UpdateFromDTO(votoUpdate);
            await _uow.CommitAsync();
        }
        public async Task DeleteVotoAsync(int pesquisaId, int eleitorId)
        {
            var voto = await _uow.IntencaoDeVotoRepository.GetByIdAsync(pesquisaId, eleitorId)
                ?? throw new InvalidOperationException($"A intenção de voto do eleitor de id {eleitorId} " +
                $"da pesquisa de id {pesquisaId} não existe.");

            _uow.IntencaoDeVotoRepository.Delete(voto);
            await _uow.CommitAsync();
        }

        private ClasseSocial IdentificaClasseSocial(decimal renda)
        {
            return renda switch
            {
                <= 2000 => ClasseSocial.Baixa,
                <= 10000 => ClasseSocial.Media,
                _ => ClasseSocial.Alta,
            };
        }
        private FaixaEtaria IdentificaFaixaEtaria(int idade)
        {
            return idade switch
            {
                >= 16 and <= 29 => FaixaEtaria.Jovem,
                > 29 and <= 59 => FaixaEtaria.Adulto,
                _ => FaixaEtaria.Idoso,
            };
        }
        private Dictionary<TEnum, decimal> CalculaDistribuicao<T, TEnum>
            (IEnumerable<T> lista, Func<T, TEnum> func, int total)
            where TEnum : notnull
        {
            return lista
                 .Select(i => func(i))
                 .GroupBy(i => i)
                 .ToDictionary(g => g.Key, g => CalculaPorcentagem(g.Count(), total));
        }
        private decimal CalculaPorcentagem(int total, decimal totalGeral)
        {
            return total == 0 ? 0 : 100 * (total / (decimal)totalGeral);
        }
    }
}
