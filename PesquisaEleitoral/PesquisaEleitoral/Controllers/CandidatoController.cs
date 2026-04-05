using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PesquisaEleitoral.DTOs;
using PesquisaEleitoral.DTOs.Candidatos;
using PesquisaEleitoral.DTOs.Mapping;
using PesquisaEleitoral.Models;
using PesquisaEleitoral.Repositories.Interfaces;

namespace PesquisaEleitoral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatoController : ControllerBase
    {
        private IUnitOfWork _uow;
        public CandidatoController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        //Pega quantidade X determinada pelo parâmetro take que é passado pela Querry.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidatoResponseDTO>>> GetPaged(int take) 
        {
            if (take < 1 || take > 100)
            {
                return BadRequest("O parâmetro 'take' deve estar entre 1 e 100.");
            }

            var candidatos = await _uow.CandidatoRepository.GetPagedAsync(take);
            var candidatosResponseDto = candidatos.ToCandidatosResponseDTOList();

            return Ok(candidatosResponseDto);
        }

        //Pega Candidato pelo id
        [HttpGet("{id}",Name="GetCandidatoById")]
        public async Task<ActionResult<CandidatoResponseDTO>> GetById(int id)
        {
            var candidato = await _uow.CandidatoRepository.GetByIdAsync(id);
            if (candidato == null) 
            {
                return NotFound();
            }
            var candidatoResponseDto = candidato.ToCandidatoResponseDTO();
            return candidatoResponseDto;
        }

        //Adiciona Candidato no sistema e retorna um DTO dele para o usuário
        [HttpPost]
        public async Task<ActionResult> Post(CandidatoDTO candidatoDto)
        {
            if(candidatoDto is null)
            {
                return BadRequest();
            }
            var candidato = candidatoDto.ToCandidato();
            var novoCandidato = _uow.CandidatoRepository.Create(candidato);

            await _uow.CommitAsync();

            var candidatoResponseDto = novoCandidato.ToCandidatoResponseDTO();

            return CreatedAtRoute("GetCandidatoById", new { id = candidatoResponseDto}, candidatoResponseDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, CandidatoPutDTO candidatoPutDto)
        {
            if(candidatoPutDto.CandidatoId != id)
            {
                return BadRequest("O id não coincide");
            }
            var candidato = candidatoPutDto.ToCandidato();
            _uow.CandidatoRepository.Update(candidato);
            await _uow.CommitAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var candidato = await _uow.CandidatoRepository.GetByIdAsync(id);
            if(candidato is null)
            {
                return NotFound();
            }
            _uow.CandidatoRepository.Delete(candidato);
            return NoContent();
        }
    }
}
