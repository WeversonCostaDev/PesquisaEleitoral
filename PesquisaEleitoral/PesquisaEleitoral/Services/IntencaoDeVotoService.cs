using PesquisaEleitoral.DTOs.Estatisticas;
using PesquisaEleitoral.DTOs.IntencaoDeVotos;
using PesquisaEleitoral.DTOs.Mapping;
using PesquisaEleitoral.Enums;
using PesquisaEleitoral.Models;
using PesquisaEleitoral.Repositories.Interfaces;
using PesquisaEleitoral.Services;

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

            var estatisticas = await _uow.IntencaoDeVotoRepository.GetEstatisticaAsync(candidato.CandidatoId);
            var escolaridade = await _uow.IntencaoDeVotoRepository.GetDistribuicaoEscolaridadeAsync(candidato.CandidatoId);
            var sexo = await _uow.IntencaoDeVotoRepository.GetDistribuicaoSexoAsync(candidato.CandidatoId);

            var result = new PerfilEleitoresDTO
            {
                CandidatoId = candidatoId,
                Nome = candidato.Nome,
                TotalVotos = estatisticas.TotalVotos,
                PorcentagemVotos = estatisticas.PorcentagemVotos,
                RendaMedia = estatisticas.RendaMedia,
                IdadeMedia = estatisticas.IdadeMedia,
                DistribuicaoEscolaridade = escolaridade,
                DistribuicaoSexo = sexo

            };
          return result;
        }
        public async Task<IEnumerable<IntencaoDeVoto>> GetPagedAsync(int take)
        {
            return await _uow.IntencaoDeVotoRepository.GetPagedAsync(take);
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
                .JaVotou(intencaoDto.EleitorId);
            
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
    }
}
