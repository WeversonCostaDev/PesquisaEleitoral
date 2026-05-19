using Microsoft.AspNetCore.Mvc;
using PesquisaEleitoral_v2.DTOs.Estatiscas;
using PesquisaEleitoral_v2.DTOs.IntencoesDeVoto;
using PesquisaEleitoral_v2.DTOs.Mapping;
using PesquisaEleitoral_v2.DTOs.Pesquisas;
using PesquisaEleitoral_v2.Enums;
using PesquisaEleitoral_v2.Repositories.Interfaces;
using PesquisaEleitoral_v2.Services;

namespace PesquisaEleitoral_v2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PesquisasController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IPesquisaService _pesquisaService;
        
        public PesquisasController(IUnitOfWork uow, IPesquisaService pesquisaService)
        {
            _uow= uow;
            _pesquisaService = pesquisaService;
        }

        [HttpGet("{pesquisaId}", Name = "pesquisa")]
        public async Task<ActionResult> GetPesquisaById(int pesquisaId) 
        {
            var pesquisa = await _uow.PesquisaRepository.GetByIdAsync(pesquisaId);
            
            if (pesquisa is null) return NotFound($"Pesquisa de id {pesquisaId} não encontrada.");

            var response = pesquisa.ToPesquisaResponseDTO();

            return Ok(response);
        }
       
        [HttpGet("{pesquisaId}/eleitores/{eleitorId}/voto", Name = "voto")]
        public async Task<ActionResult<IntencaoDeVotoResponseDTO>> GetVotoById(
            int pesquisaId,
            int eleitorId)
        {
            var response = await _pesquisaService.GetVotoByIdAsync(pesquisaId, eleitorId);
            return Ok(response);
        }
        
        [HttpGet("{pesquisaId}/estatisticas/candidatos")]
        public async Task<ActionResult<IEnumerable<EstatisticaVotoResponseDTO>>> GetEstatisticas(
            int pesquisaId, Regiao? regiao = null)
        {
            var response = await _pesquisaService.GetEstatisticaPorCandidatoAsync(
                pesquisaId, regiao);
            return Ok(response);
        }
        
        [HttpGet("{pesquisaId}/candidatos/{candidatoId}/perfil-eleitores")]
        public async Task<ActionResult<PerfilEleitoresDTO>> GetPerfilEleitores(
            int pesquisaId, int candidatoId)
        {
            var response = await _pesquisaService.GetPerfilEleitores(pesquisaId, candidatoId);
            return Ok(response);
        }
        
        [HttpPost]
        public async Task<ActionResult> CriaPesquisa(PesquisaDTO pesquisaDto)
        {
            var pesquisa = pesquisaDto.ToPesquisa();
            _uow.PesquisaRepository.Create(pesquisa);
            await _uow.CommitAsync();

            var response = pesquisa.ToPesquisaResponseDTO();

            return CreatedAtRoute("pesquisa", new {pesquisaId = response.PesquisaId}, response);
        }

        [HttpPost("candidatos")]
        public async Task<ActionResult> AdicionaCandidatoAPesquisa(PesquisaCandidatoDTO pc)
        {
            var pesquisa = await _uow.PesquisaRepository.GetByIdAsync(pc.PesquisaId);
            if (pesquisa is null)
                throw new InvalidOperationException($"Pesquisa de Id {pc.PesquisaId} não encontrada.");
            var candidato = await _uow.CandidatoRepository.GetByIdAsync(pc.CandidatoId);
            if (candidato is null)
                throw new InvalidOperationException($"Candidato de Id {pc.CandidatoId} não encontrada.");
            pesquisa.Candidatos.Add(candidato);
            await _uow.CommitAsync();
            return NoContent();
        }

        [HttpPost("eleitores/voto")]
        public async Task<ActionResult> CriaVotoNaPesquisa(IntencaoDeVotoDTO voto)
        {
            var response = await _pesquisaService.VotarAsync(voto);
            return CreatedAtRoute("voto",new 
            {
                pesquisaId = response.Pesquisa.PesquisaId,
                eleitorId = response.Eleitor.EleitorId,
            }, response);
        }

        [HttpPut("eleitores/voto")]
        public async Task<ActionResult> Put(IntencaoDeVotoUpdateDTO votoUpdate)
        {
            await _pesquisaService.AtualizaVotoEleitorAsync(votoUpdate);
            return NoContent();
        }
        
        [HttpDelete("{pesquisaId}")]
        public async Task<ActionResult> Delete(int pesquisaId)
        {
            var pesquisa = await _uow.PesquisaRepository.GetByIdAsync(pesquisaId);
            if (pesquisa is null) 
                throw new InvalidOperationException($"Pesquisa de id {pesquisaId} não existe.");
            _uow.PesquisaRepository.Delete(pesquisa);
            await _uow.CommitAsync();
            return NoContent();
        }
        
        [HttpDelete("{pesquisaId}/eleitores/{eleitorId}/voto")]
        public async Task<ActionResult> DeleteVoto(int pesquisaId, int eleitorId)
        {
            await _pesquisaService.DeleteVotoAsync(pesquisaId, eleitorId);
            return NoContent();
        }
    }
}
