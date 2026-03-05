using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Service;
using RoyalGames.Dtos.PlataformaDto;


namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlataformaController : ControllerBase
    {
        private readonly PlataformaService _service;

        public PlataformaController(PlataformaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerPlataformaDto>> Listar()
        {
            List<LerPlataformaDto> plataformas = _service.Listar();
            return Ok(plataformas);
        }

        [HttpGet("{id}")]
        public ActionResult<LerPlataformaDto> ObterPorId(int id)
        {
            LerPlataformaDto plataforma = _service.ObterPorId(id);
            if (plataforma == null)
            {
                return NotFound();
            }

            return Ok(plataforma);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Adicionar(CriarPlataformaDto criarDto)
        {
            try
            {
                _service.Adicionar(criarDto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}