using Microsoft.AspNetCore.Mvc;
using PesquisaEleitoral.DTOs.Estatisticas;
using PesquisaEleitoral.DTOs.IntencaoDeVotos;
using PesquisaEleitoral.DTOs.Mapping;
using PesquisaEleitoral.Enums;
using PesquisaEleitoral.Models;
using PesquisaEleitoral.Repositories.Interfaces;
using PesquisaEleitoral.Service;
using PesquisaEleitoral.Services;

namespace PesquisaEleitoral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntencaoDeVotoController : ControllerBase
    {
        private readonly IIntencaoDeVotoService _intencaoDeVotoService;

        public IntencaoDeVotoController(IIntencaoDeVotoService intencaoDeVotoService)
        {
            _intencaoDeVotoService = intencaoDeVotoService;
        }

        [HttpGet] 
        public async Task<ActionResult<IEnumerable<IntencaoDeVotoResponseDTO>>> GetPaged(int take)
        {
            var intencoesDeVoto = await _intencaoDeVotoService.GetPagedAsync(take);
            var intencoesDeVotoResponseDto = intencoesDeVoto.ToIntencaoDeVotoResponseDTOList();
            return Ok(intencoesDeVotoResponseDto);
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<IntencaoDeVotoResponseDTO>> GetById(int id)
        {
            var intencaoDeVoto = await _intencaoDeVotoService.GetByIdAsync(id);
            if(intencaoDeVoto is null)
                return NotFound("Não encontrado!");
 
            return Ok(intencaoDeVoto);
        }

        [HttpGet("estatisticas")]
        public async Task<ActionResult<IEnumerable<EstatisticaVotoResponseDTO>>> GetEstatistica(Regiao? regiao)
        {
            var listaDeVotosPorCandidato = await _intencaoDeVotoService.EstatisticaPorCandidatoAsync(regiao);

            return Ok(listaDeVotosPorCandidato);
        }

        [HttpGet("perfil/eleitores")]
        public async Task<ActionResult<PerfilEleitoresDTO>> GetPerfilEleitores(int candidatoId)
        {
            var perfil = await _intencaoDeVotoService.GetPerfilEleitores(candidatoId);
            return Ok(perfil);
        }

        [HttpPost]
        public async Task<ActionResult> Post(IntencaoDeVotoDTO intencaoDeVotoDto)
        {   

            var intencaoResponseDto = await _intencaoDeVotoService.CreateAsync(intencaoDeVotoDto);

            return CreatedAtRoute(nameof(GetById), new {id = intencaoResponseDto.IntencaoDeVotoId}, intencaoResponseDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, IntencaoDeVotoPutDTO intencaoDeVotoPutDto)
        {
            if (intencaoDeVotoPutDto.IntencaoDeVotoId != id)
                return BadRequest("O id não coincide");

            await _intencaoDeVotoService.UpdateAsync(intencaoDeVotoPutDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) 
        {
            await _intencaoDeVotoService.DeleteAsync(id);
            return NoContent();
        }
    }
}
    