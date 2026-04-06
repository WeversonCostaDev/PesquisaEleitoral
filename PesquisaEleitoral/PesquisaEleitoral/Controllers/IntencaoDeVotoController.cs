using Microsoft.AspNetCore.Mvc;
using PesquisaEleitoral.DTOs.Estatisticas;
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


        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<IntencaoDeVotoResponseDTO>> GetById(int id)
        {
            var intencaoDeVoto = await _uow.IntencaoDeVotoRepository.GetByIdFullAsync(id);
            if(intencaoDeVoto is null)
            {
                return NotFound("Não encontrado!");
            }

            var intencaoDeVotoResponseDto = intencaoDeVoto.ToIntencaoDeVotoResponseDTO(); 
            return Ok(intencaoDeVotoResponseDto);
        }

        [HttpGet("estatisticas")]
        public async Task<ActionResult<IEnumerable<EstatisticaVotoResponseDTO>>> GetEstatistica(Regiao? regiao)
        {
            var listaDeVotosPorCandidato = await _uow.IntencaoDeVotoRepository.EstatisticaDeVotoPorCandidatoAsync(regiao);

            return Ok(listaDeVotosPorCandidato);
        }

        [HttpPost]
        public async Task<ActionResult> Post(IntencaoDeVotoDTO intencaoDeVotoDto)
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
            var intencaoDeVotoAtualizado = await _uow.IntencaoDeVotoRepository.GetByIdFullAsync(intencaoDeVoto.IntencaoDeVotoId);
            var intencaoDeVotoResponseDto = intencaoDeVotoAtualizado!.ToIntencaoDeVotoResponseDTO();

            return CreatedAtRoute(nameof(GetById), new {id = intencaoDeVotoResponseDto.IntencaoDeVotoId}, intencaoDeVotoResponseDto);
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
    