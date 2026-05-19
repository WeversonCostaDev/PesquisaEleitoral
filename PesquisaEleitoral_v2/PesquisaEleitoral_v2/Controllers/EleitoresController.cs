using Microsoft.AspNetCore.Mvc;
using PesquisaEleitoral_v2.DTOs.Eleitores;
using PesquisaEleitoral_v2.DTOs.Mapping;
using PesquisaEleitoral_v2.Pagination;
using PesquisaEleitoral_v2.Repositories.Interfaces;
using System.Text.Json;

namespace PesquisaEleitoral_v2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EleitoresController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        public EleitoresController(IUnitOfWork uow) 
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<IEnumerable<EleitorResponseDTO>> GetPagedAsync(
            [FromQuery] EleitorParameters parameters)
        {
            var eleitores = await _uow.EleitorRepository.GetPagedAsync(parameters);

            var metadata = new
            {
                eleitores.CurrentPage,
                eleitores.TotalPage,
                eleitores.HasPrevious,
                eleitores.HasNext,
            };

            Response.Headers.Append("Pagination", JsonSerializer.Serialize(metadata));

            var response = eleitores.ToEleitoresResponseDTOList();
            return response;
        }

        [HttpGet("{id}", Name ="GetEleitorById")]
        public async Task<ActionResult<EleitorResponseDTO>> GetById(int id)
        {
            var eleitor = await _uow.EleitorRepository.GetByIdAsync(id);
            if(eleitor is null)
            {
                return NotFound($"Eleitor de id {id} não encontrado!");
            }
            var response = eleitor.ToEleitorResponseDTO();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post(EleitorDTO eleitorDto)
        {
            var eleitor = eleitorDto.ToEleitor();
            eleitor = _uow.EleitorRepository.Create(eleitor);
            await _uow.CommitAsync();

            var response = eleitor.ToEleitorResponseDTO();

            return CreatedAtRoute("GetEleitorById", new {id = response.EleitorId}, response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, EleitorUpdateDTO eleitorDto)
        {   if (id != eleitorDto.EleitorId) 
                return BadRequest("Os números de id não coincidem!");

            var eleitor = await _uow.EleitorRepository.GetByIdAsync(id);

            if (eleitor is null)
            {
                return NotFound($"Eleitor de id {eleitorDto.EleitorId} não encontrado!");
            }
            eleitor.UpdateFromDTO(eleitorDto);
            await _uow.CommitAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var eleitor = await _uow.EleitorRepository.GetByIdAsync(id);
            if (eleitor is null) return NotFound($"Eleitor de id {id} não encontrado!");

            _uow.EleitorRepository.Delete(eleitor);
            await _uow.CommitAsync();

            return NoContent();
        }
    }
}
