using Microsoft.AspNetCore.Mvc;
using PesquisaEleitoral.DTOs.IntencaoDeVotos;
using PesquisaEleitoral.DTOs.Mapping;
using PesquisaEleitoral.Enums;
using PesquisaEleitoral.Repositories.Interfaces;

namespace PesquisaEleitoral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntencaoDeVotoController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public IntencaoDeVotoController(IUnitOfWork uow)
        {
            _uow = uow;
        }
     

        [HttpGet("{id}", Name = "GetIntencaoDeVotoById")]
        public async Task<ActionResult<IntencaoDeVotoResponseDTO>> GetById(int id)
        {
            var intencaoDeVoto = await _uow.IntencaoDeVotoRepository.GetByIdAsync(id);
            if(intencaoDeVoto is null)
            {
                return NotFound("Não encontrado!");
            }

            var intencaoDeVotoResponseDto = intencaoDeVoto.ToIntencaoDeVotoResponseDTO(); 
            return Ok(intencaoDeVotoResponseDto);
        } 

        [HttpPost]
        public async Task<ActionResult<IntencaoDeVotoResponseDTO>> Post(IntencaoDeVotoDTO intencaoDeVotoDto)
        {   
            if (intencaoDeVotoDto is null)
            {
                return BadRequest("Entrada de dados inválida.");
            }

            //Regra de negócio -> adicione em service
            bool jaVotou = await _uow.IntencaoDeVotoRepository.AnyAsync(iv => iv.EleitorId == intencaoDeVotoDto.EleitorId);
            if(jaVotou)
            {
                return BadRequest($"Eleitor já efetuou o seu voto!\nSe quiser tente atualizar.");
            }

            var intencaoDeVoto = intencaoDeVotoDto.ToIntencaoDeVoto();
            _uow.IntencaoDeVotoRepository.Create(intencaoDeVoto);
            await _uow.CommitAsync();
            return CreatedAtRoute("GetIntencaoDeVotoById", new {id = intencaoDeVoto.IntencaoDeVotoId}, intencaoDeVoto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, IntencaoDeVotoPutDTO intencaoDeVotoPutDto)
        {
            if (intencaoDeVotoPutDto.IntencaoDeVotoId == id)
            {
                return BadRequest("O id não coincide");
            }
            var intencaoDeVoto = intencaoDeVotoPutDto.ToIntencaoDeVoto();
            _uow.IntencaoDeVotoRepository.Update(intencaoDeVoto);
            await _uow.CommitAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) 
        {
            var intencaoDeVoto = await _uow.IntencaoDeVotoRepository.GetByIdAsync(id);
            if (intencaoDeVoto is null)
            {
                return NotFound("Não encontrado!");
            }
            _uow.IntencaoDeVotoRepository.Delete(intencaoDeVoto);
            await _uow.CommitAsync();
            return NoContent();
        }
    }
}
    