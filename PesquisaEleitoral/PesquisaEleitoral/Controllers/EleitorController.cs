
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PesquisaEleitoral.DTOs.Candidatos;
using PesquisaEleitoral.DTOs.Eleitores;
using PesquisaEleitoral.DTOs.Mapping;
using PesquisaEleitoral.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PesquisaEleitoral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EleitorController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        public EleitorController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EleitorResponseDTO>>> GetPaged([BindRequired][Range(1, 100)] int take)
        {
            var eleitores = await _uow.EleitorRepository.GetPagedAsync(take);
            var eleitoresResponseDto = eleitores.ToEleitoresResponseDTOList();

            return Ok(eleitoresResponseDto);
        }

        [HttpGet("{id}", Name = "GetEleitorById")]
        public async Task<ActionResult<EleitorResponseDTO>> GetById(int id)
        {
            var eleitor = await _uow.EleitorRepository.GetByIdAsync(id);
            if (eleitor is null)
            {
                return NotFound("Eleitor não encontrado!");
            }
            var eleitorResponseDto = eleitor.ToEleitorResponseDTO();
            return eleitorResponseDto;
        }

        [HttpPost]
        public async Task<ActionResult> Post(EleitorDTO eleitorDto)
        {
            if (eleitorDto is null)
            {
                return BadRequest("Entrada de dados inválida!");
            }
            var eleitor = eleitorDto.ToEleitor();
            var novoEleitor = _uow.EleitorRepository.Create(eleitor);

            await _uow.CommitAsync();

            var eleitorResponseDto = novoEleitor.ToEleitorResponseDTO();

            return CreatedAtRoute("GetEleitorById", new { id = eleitorResponseDto.EleitorId }, eleitorResponseDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, EleitorPutDTO eleitorPutDto)
        {
            if (id != eleitorPutDto.EleitorId)
            {
                return BadRequest("Os números de Id não coincidem.");
            }

            var eleitor = eleitorPutDto.ToEleitor();
            _uow.EleitorRepository.Update(eleitor);
            await _uow.CommitAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var eleitor = await _uow.EleitorRepository.GetByIdAsync(id);
            if (eleitor is null)
            {
                return NotFound("Eleitor não encontrado!");
            }

            _uow.EleitorRepository.Delete(eleitor);

            await _uow.CommitAsync();
            return NoContent();
        }
    }
}
