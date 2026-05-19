using Microsoft.AspNetCore.Mvc;
using PesquisaEleitoral_v2.DTOs.Candidatos;
using PesquisaEleitoral_v2.DTOs.Mapping;
using PesquisaEleitoral_v2.Pagination;
using PesquisaEleitoral_v2.Repositories.Interfaces;
using System.Text.Json;

namespace PesquisaEleitoral_v2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatosController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        public CandidatosController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidatoResponseDTO>>> GetPagedAsync(
            [FromQuery] CandidatoParameters parameters)
        {
            var candidatos = await _uow.CandidatoRepository.GetPagedAsync(parameters);
            var metadata = new
            {
                candidatos.CurrentPage,
                candidatos.TotalPage,
                candidatos.HasPrevious,
                candidatos.HasNext,
            };
            Response.Headers.Append("Pagination", JsonSerializer.Serialize(metadata));
            var response = candidatos.ToCandidatosResponseDTOList();
            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetCandidatoById")]
        public async Task<ActionResult> GetById(int id)
        {
            var candidato = await _uow.CandidatoRepository.GetByIdAsync(id);
            if (candidato is null)
            {
                return NotFound($"Candidator de id {id} não encontrado!");
            }
            var response = candidato.ToCandidatoResponseDTO();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CandidatoDTO candidatoDto)
        {
            var candidato = candidatoDto.ToCandidato();
            candidato = _uow.CandidatoRepository.Create(candidato);
            await _uow.CommitAsync();

            var response = candidato.ToCandidatoResponseDTO();

            return CreatedAtRoute("GetCandidatoById", new { id = response.CandidatoId }, response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, CandidatoUpdateDTO candidatoDto)
        {
            if (id != candidatoDto.CandidatoId)
                return BadRequest("Os números de id não coincidem!");

            var candidato = await _uow.CandidatoRepository.GetByIdAsync(id);

            if (candidato is null)
            {
                return NotFound($"Eleitor de id {candidatoDto.CandidatoId} não encontrado!");
            }
            candidato.UpdateFromDTO(candidatoDto);
            await _uow.CommitAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var eleitor = await _uow.CandidatoRepository.GetByIdAsync(id);
            if (eleitor is null) return NotFound($"Eleitor de id {id} não encontrado!");

            _uow.CandidatoRepository.Delete(eleitor);
            await _uow.CommitAsync();
            return NoContent();
        }
    }
}
