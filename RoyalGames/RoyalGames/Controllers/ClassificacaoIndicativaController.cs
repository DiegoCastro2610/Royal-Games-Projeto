using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Service;
using RoyalGames.Dtos.ClassificacaoIndicativaDto;
using RoyalGames.Dtos.UsuarioDto;
using RoyalGames.Exceptions;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassificacaoIndicativaController : ControllerBase
    {
        private readonly ClassificacaoIndicativaService _service;

        public ClassificacaoIndicativaController(ClassificacaoIndicativaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerClassificacaoIndicativaDto>> Listar()
        {
            List<LerClassificacaoIndicativaDto> Classificacao = _service.Listar();
            return Ok(Classificacao);
        }

        [HttpGet("{id}")]
        public ActionResult<LerClassificacaoIndicativaDto> ObterPorId(int id)
        {
            try
            {
                LerClassificacaoIndicativaDto ClassificacaoDto = _service.ObterPorId(id);
                return Ok(ClassificacaoDto);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<LerClassificacaoIndicativaDto> Adicionar(CriarClassificacaoIndicativaDto Classificacao)
        {
            try
            {
                LerClassificacaoIndicativaDto CriarClassificacao = _service.Adicionar(Classificacao);
                return StatusCode(201);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
