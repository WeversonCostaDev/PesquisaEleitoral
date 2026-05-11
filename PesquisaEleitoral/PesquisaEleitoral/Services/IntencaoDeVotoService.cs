using PesquisaEleitoral.DTOs.Estatisticas;
using PesquisaEleitoral.DTOs.IntencaoDeVotos;
using PesquisaEleitoral.DTOs.Mapping;
using PesquisaEleitoral.Enums;
using PesquisaEleitoral.Models;
using PesquisaEleitoral.Pagination.Interfaces;
using PesquisaEleitoral.Repositories.Interfaces;
using PesquisaEleitoral.Services;
using System.Linq.Expressions;
using System.Numerics;

namespace PesquisaEleitoral.Service
{
    public class IntencaoDeVotoService : IIntencaoDeVotoService
    {
        private readonly IUnitOfWork _uow;
        public IntencaoDeVotoService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IntencaoDeVotoResponseDTO> GetByIdAsync(int id)
        {
            var intencaoDeVoto = await _uow.IntencaoDeVotoRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Intenção de voto não encontrada.");

            var intencoesDeVotoDto = intencaoDeVoto.ToIntencaoDeVotoResponseDTO();
            return intencoesDeVotoDto;
        }
        public async Task<PerfilEleitoresDTO> GetPerfilEleitores(int candidatoId)
        {
            var candidato = await _uow.CandidatoRepository.GetByIdAsync(candidatoId)
                ?? throw new KeyNotFoundException("Candidato não encontrado.");
            int totalGeral = await _uow.IntencaoDeVotoRepository.GetTotalDeVotosAsync();

            var estatisticas = await _uow.IntencaoDeVotoRepository.GetEstatisticaAsync(candidato.CandidatoId);
            var escolaridade = await _uow.IntencaoDeVotoRepository.GetDistribuicaoEscolaridadeAsync(candidato.CandidatoId);
            var sexo = await _uow.IntencaoDeVotoRepository.GetDistribuicaoSexoAsync(candidato.CandidatoId);
            
            //Calcula a porcentagem de votos
            decimal porcentagemVotos = CalculaPorcentagem(estatisticas.ContagemVotos, totalGeral);

            //Converte a lista de contagem de votos por escolaridade, para um dicionário com a porcentagem como valor. 
            decimal totalEscolaridade = escolaridade.Sum(obj => obj.Total);
            Dictionary<Escolaridade, decimal> dictEscolaridade = escolaridade.ToDictionary(
                item => item.Escolaridade, 
                item => totalEscolaridade = CalculaPorcentagem(item.Total, totalEscolaridade));

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
                CandidatoId = candidatoId,
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
        public async Task<IPagedList<IntencaoDeVoto>> GetPagedAsync(IQueryStringPagination parameters)
        {
            return await _uow.IntencaoDeVotoRepository.GetPagedAsync(parameters);
        }
        public async Task<IEnumerable<EstatisticaVotoResponseDTO>> EstatisticaPorCandidatoAsync(Regiao? regiao = null)
        {
            return await _uow.IntencaoDeVotoRepository.EstatisticaPorCandidatoAsync(regiao);
        }
        public async Task<IntencaoDeVotoResponseDTO> CreateAsync(IntencaoDeVotoDTO intencaoDto)
        {
            var verifyEleitor = await _uow.EleitorRepository
                .VerifyAsync(e => e.EleitorId == intencaoDto.EleitorId);

            if(!verifyEleitor)
                throw new KeyNotFoundException("Eleitor não existe.");

            var verifyCandidato = await _uow.CandidatoRepository
                .VerifyAsync(c => c.CandidatoId == intencaoDto.CandidatoId);
            
            if (!verifyCandidato)
                throw new KeyNotFoundException("Candidato não existe.");

            var verifyVotoEleitor = await _uow.IntencaoDeVotoRepository
                .JaVotouAsync(intencaoDto.EleitorId);
            
            if (verifyVotoEleitor)
                throw new InvalidOperationException("Eleitor já votou .");

            var intencao = intencaoDto.ToIntencaoDeVoto(); 
            intencao = _uow.IntencaoDeVotoRepository.Create(intencao);
            await _uow.CommitAsync();

            intencao = await _uow.IntencaoDeVotoRepository
                .GetByIdAsync(intencao.IntencaoDeVotoId);

            if (intencao is null) throw new KeyNotFoundException("Erro ao recuperar a intenção de voto");

            var intencaoResponseDto = intencao.ToIntencaoDeVotoResponseDTO();
          
            return intencaoResponseDto;
        }
        public async Task UpdateAsync(IntencaoDeVotoPutDTO intencaoDeVotoPutDto)
        {
            var intencao = await _uow.IntencaoDeVotoRepository
                .GetByIdAsync(intencaoDeVotoPutDto.IntencaoDeVotoId);

            if (intencao is null)
                throw new InvalidOperationException("Não existe registro dessa intenção de voto.");

            var eleitorExiste = await _uow.EleitorRepository
                .VerifyAsync(e => e.EleitorId == intencaoDeVotoPutDto.EleitorId);
            if (!eleitorExiste)
                throw new InvalidOperationException("Eleitor não encontrado.");

            var candidatoExiste = await _uow.CandidatoRepository
                .VerifyAsync(c => c.CandidatoId == intencaoDeVotoPutDto.CandidatoId);
            if (!candidatoExiste)
                throw new InvalidOperationException("Candidato não encontrado.");

            intencao.UpdateFromDTO(intencaoDeVotoPutDto);
            await _uow.CommitAsync();

        }   
        public async Task DeleteAsync(int id)
        {
            var intencao = await _uow.IntencaoDeVotoRepository.GetByIdAsync(id);
            if (intencao is null)
                throw new InvalidOperationException("Registro de voto não encontrado.");
            _uow.IntencaoDeVotoRepository.Delete(intencao);
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
                >29 and <=59 => FaixaEtaria.Adulto,
                _ => FaixaEtaria.Idoso,
            };
        }
        private Dictionary<TEnum, decimal> CalculaDistribuicao<T,TEnum>
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
