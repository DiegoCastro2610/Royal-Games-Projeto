using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Service;
using RoyalGames.Exceptions;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogJogoController : ControllerBase
    {
        private readonly LogAlteracaoJogoService _service;

        public LogJogoController(LogAlteracaoJogoService service)
        {
            _service = service;
        }

        [HttpGet]

        public ActionResult Listar()
        {
            return Ok(_service.Listar());
        }

        [HttpGet("jogo/{id}")]

        public ActionResult ListarProduto(int id)
        {
            try
            {
                return Ok(_service.ListarPorJogo(id));
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
