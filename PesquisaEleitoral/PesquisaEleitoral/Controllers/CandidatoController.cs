using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PesquisaEleitoral.DTOs;
using PesquisaEleitoral.DTOs.Mapping;
using PesquisaEleitoral.Models;
using PesquisaEleitoral.Repositories.Interfaces;

namespace PesquisaEleitoral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatoController : ControllerBase
    {
        private IUnitOfWork _uof;
        public CandidatoController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        //Pega quantidade X determinada pelo parâmetro take que é passado pela Querry.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidatoResponseDTO>>> GetAll(int take) 
        {
            if (take < 1 || take > 100)
            {
                return BadRequest("O parâmetro 'take' deve estar entre 1 e 100.");
            }

            var candidatos = await _uof.CandidatoRepository.GetAll(take);
            var candidatosResponseDto = candidatos.ToCandidatosResponseDTOList();

            return Ok(candidatosResponseDto);
        }

        //Pega Candidato pelo id
        [HttpGet("{id}",Name="GetCandidatoById")]
        public async Task<ActionResult<CandidatoResponseDTO>> GetById(int id)
        {
            var candidato = await _uof.CandidatoRepository.GetById(id);
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
            var novoCandidato = _uof.CandidatoRepository.Create(candidato);

            await _uof.CommitAsync();

            var candidatoResponseDto = novoCandidato.ToCandidatoResponseDTO();

            return CreatedAtRoute("GetCandidatoById", new { id = candidatoResponseDto}, candidatoResponseDto);
        }

        
    }
}
